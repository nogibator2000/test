using App.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Services
{
    public class TokenService:ITokenService
    {
        public string GetToken(string email, string role)
        {
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, email),
                new Claim("roles", role),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = "issuer",
                    Audience = "audience",
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my security key")),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return stringToken;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
