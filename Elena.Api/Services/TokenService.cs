using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Elena.Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string username, string email, string role)
        {
            // Verificar se os parâmetros não são nulos
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username), "O username não pode ser nulo ou vazio.");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "O email não pode ser nulo ou vazio.");
            }

            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException(nameof(role), "O role não pode ser nulo ou vazio.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }

}
