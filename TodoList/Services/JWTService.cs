using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Database;
using TodoList.Database.Models;
using TodoList.RequestHandler.Requests;
using TodoList.RequestHandler.Responces;

namespace TodoList.Services
{
    public class JWTService
    {
        private readonly IConfiguration _configuration;
        private readonly DapperUtility _dapperUtility;
        public JWTService(IConfiguration configuration, DapperUtility dapperUtility)
        {
            _configuration = configuration;
            _dapperUtility = dapperUtility;
        }

        public async Task<LoginResponce?> Authenticate(LoginRequest request)
        {
            string sql = "SELECT id,username,password FROM Users WHERE username = @username";
            using (var connection = _dapperUtility.GetConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { username = request.Username });
                if (user is null || !PasswordHasher.VerifyPassword(request.Password, user.password)) return null;
            }

            var issuer = _configuration["JWTConfiguration:Issuer"];
            var audience = _configuration["JWTConfiguration:Audience"];
            var key = _configuration["JWTConfiguration:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JWTConfiguration:TokenValidityMinutes");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, request.Username)
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponce
            {
                AccessToken = accessToken,
                Username = request.Username,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
    }
}
