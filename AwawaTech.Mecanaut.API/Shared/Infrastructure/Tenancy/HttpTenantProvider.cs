using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;                 // IWebHostEnvironment
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;

namespace AwawaTech.Mecanaut.API.Shared.Infrastructure.Tenancy;

/// <summary>
/// Obtiene el tenant a partir del header <c>X-Tenant</c>.
/// En modo <c>Development</c> permite un GUID por defecto si el header falta.
/// </summary>
public class HttpTenantProvider(IHttpContextAccessor accessor, IWebHostEnvironment env) : ITenantProvider
{
    public TenantId Current
    {
        get
        {
            var raw = accessor.HttpContext?.Request.Headers["X-Tenant"].FirstOrDefault();

            // Valor por defecto SOLO cuando estamos en Development
            if (string.IsNullOrWhiteSpace(raw) && env.IsDevelopment())
                raw = "00000000-0000-0000-0000-000000000001";

            if (!Guid.TryParse(raw, out var id))
                throw new UnauthorizedAccessException("Missing or invalid X-Tenant header");

            return new TenantId(id);
        }
    }
}
