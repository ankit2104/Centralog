using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthService.Jwt
{
    public interface IJwtService
    {
        public JwtSecurityToken CreateToken(List<Claim> authClaims);
        public string GenerateRefreshToken();
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
