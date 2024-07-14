using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Softka.Infrastructure.Data;
using Softka.Models.DTOs;


namespace Authcontroller
{
    public class Authcontroller : Controller
    {
        private readonly BaseContext _context;
        public Authcontroller (BaseContext context)
        {
            {
                _context = context;
            }
        }
        [HttpPost]
        public async Task <IActionResult> Auth ([FromBody] UserDto user)
        {
            var User = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email  && u.Password == user.Password); //In this line i need the Hash password
            if (user == null )
            {
                return Unauthorized("Please fill  all fields");
            }
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@Environment.GetEnvironmentVariable("JwtToken"))); //variable key
            var SigninCredentials = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
            //This apart id for permissions
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                new Claim("id", User.Id.ToString())
            };
            //Add token opstions
            var TokenOptions = new JwtSecurityToken(
                issuer : @Environment.GetEnvironmentVariable("Issuer"), 
                audience : @Environment.GetEnvironmentVariable("Audience"),
                claims : Claims,
                expires : DateTime.Now.AddMinutes(30),
                signingCredentials : SigninCredentials
            );
            //Token Generated
            var TokenString = new JwtSecurityTokenHandler().WriteToken(TokenOptions);
            return Ok (new{Token = TokenString});

            }
    }
}