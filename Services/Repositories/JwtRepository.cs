using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Softka.Infrastructure.Data;
using Softka.Models.DTOs;
using Softka.Services;
using Microsoft.Extensions.Configuration;

namespace Softkat.Services
{
    public class JwtRepository : IJwtRepository
    {
        private readonly BaseContext _context;
        public JwtRepository(BaseContext context)
        {
            _context = context;
        }

        public string GenerateToken(UserDto user)
        {
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@Environment.GetEnvironmentVariable("JwtToken")));
            var signinCredentials = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
            
            
            var TokenOptions = new JwtSecurityToken
            (
                issuer: @Environment.GetEnvironmentVariable("Issuer"),
                audience: @Environment.GetEnvironmentVariable("Audience"),
                claims: new List<Claim>(),
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signinCredentials
            );

            var Tokenstring = new JwtSecurityTokenHandler().WriteToken(TokenOptions);
            
            return Tokenstring;

        }
    }
   
}

