using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.RequestHandler.Requests;
using TodoList.RequestHandler.Responces;

namespace TodoList.Services
{
    public class JWTService(IConfiguration configuration)
    {
        public LoginResponse Authenticate(LoginRequest request)
        {
            var issuer = configuration["JWTConfiguration:Issuer"];
            var audience = configuration["JWTConfiguration:Audience"];
            var key = configuration["JWTConfiguration:Key"];
            var tokenValidityMins = configuration.GetValue<int>("JWTConfiguration:TokenValidityMinutes");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Name, request.Username)
                ]),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!)),
                SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponse
            {
                AccessToken = accessToken,
                Username = request.Username,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
    }
}
