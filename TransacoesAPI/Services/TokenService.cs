using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransacoesAPI.Models;
using TransacoesAPI.Models.Entity;
using TransacoesAPI.Models.Requests;
using TransacoesAPI.Services.Interfaces;

namespace TransacoesAPI.Services
{
    public static class TokenService
    {
        public static string CreateToken(Usuario usuario, IConfiguration configuration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new
                    SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddHours(5),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
