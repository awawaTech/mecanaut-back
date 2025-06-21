using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AwawaTech.Mecanaut.API.Shared.Infrastructure.Interfaces.ASP.Filters;

/// <summary>
/// Filtro global que transforma excepciones en respuestas JSON coherentes.
/// Se ejecuta dentro del pipeline MVC, por lo que Visual Studio ya no marcará
/// las InvalidOperationException como "user-unhandled".
/// </summary>
public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        _logger.LogError(exception, "Unhandled exception in MVC pipeline");

        var status = exception switch
        {
            ValidationException           => HttpStatusCode.BadRequest,    // 400
            KeyNotFoundException          => HttpStatusCode.NotFound,      // 404
            UnauthorizedAccessException   => HttpStatusCode.Unauthorized,  // 401
            SecurityTokenException        => HttpStatusCode.Unauthorized,  // 401
            InvalidOperationException     => HttpStatusCode.Conflict,      // 409
            DbUpdateException             => HttpStatusCode.Conflict,      // 409 (constraint violations)
            _                             => HttpStatusCode.InternalServerError
        };

        var response = new
        {
            timestamp = DateTime.UtcNow,
            status    = (int)status,
            error     = status.ToString(),
            message   = exception.Message,
            path      = context.HttpContext.Request.Path.ToString()
        };

        context.Result = new JsonResult(response) { StatusCode = (int)status };
        context.ExceptionHandled = true;
    }
} 