using System;
using System.Security.Claims;

namespace AwawaTech.Mecanaut.API.Shared.Infrastructure.Interfaces.ASP.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? Guid.Parse(claim.Value) : Guid.Empty;
        }
    }
} 