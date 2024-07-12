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
        private readonly IConfiguration _configuration;
        public JwtRepository(BaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string GenerateToken(UserDto user)
        {
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
            // var Claims = new List<Claim>
            // {
            //     new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
            //     new Claim("id", user.Id.ToString())
            // };
            
            var TokenOptions = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var Tokenstring = new JwtSecurityTokenHandler().WriteToken
            (TokenOptions);
            return Tokenstring;

        }
    }
   
}

