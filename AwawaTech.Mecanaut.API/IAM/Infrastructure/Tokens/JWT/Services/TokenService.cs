using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text;
using AwawaTech.Mecanaut.API.IAM.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.IAM.Domain.Repositories;
using AwawaTech.Mecanaut.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using AwawaTech.Mecanaut.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;
using System.Linq;

namespace AwawaTech.Mecanaut.API.IAM.Infrastructure.Tokens.JWT.Services;

/**
 * <summary>
 *     The token service
 * </summary>
 * <remarks>
 *     This class is used to generate and validate tokens
 * </remarks>
 */
public class TokenService(IOptions<TokenSettings> tokenSettings, ITenantRepository tenantRepository) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;
    private readonly ITenantRepository _tenantRepository;

    /**
     * <summary>
     *     Generate token
     * </summary>
     * <param name="user">The user for token generation</param>
     * <returns>The generated Token</returns>
     */
    public string GenerateToken(User user)
    {
        var secret = _tokenSettings.Secret;
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("tenant_id", user.TenantId.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        // roles claims
        foreach (var role in user.Roles)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role.Name.ToString()));
        }
        var tokenHandler = new JsonWebTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }

    /**
     * <summary>
     *     VerifyPassword token
     * </summary>
     * <param name="token">The token to validate</param>
     * <returns>The user id if the token is valid, null otherwise</returns>
     */
    public async Task<(int userId, long tenantId)?> ValidateToken(string token)
    {
        // If token is null or empty
        if (string.IsNullOrEmpty(token))
            // Return null 
            return null;
        // Otherwise, perform validation
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        try
        {
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Expiration without delay
                ClockSkew = TimeSpan.Zero
            });

            var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;
            var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value);
            var tenantIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "tenant_id");
            var tenantId = tenantIdClaim is null ? 1L : long.Parse(tenantIdClaim.Value);
            return (userId, tenantId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    // Método privado para obtener el subscriptionPlanId usando el tenantId.

}