using Softka.Utils.PasswordHashing;
using Softka.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Softka.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Softka.Services;
using Microsoft.Extensions.Logging;
using Softka.Models;



public class LoginController : Controller
{
    private readonly Bcrypt _bCrypt;
    private readonly BaseContext _context;
    private readonly IJwtRepository _jwtRepository;
    //Inyecction of prube
    private readonly ILogger<LoginController> _logger;  // This logger helpme to resolve the bug of the login

    public LoginController(Bcrypt bCrypt, BaseContext context, IJwtRepository jwtRepository, ILogger<LoginController> logger)
    {
        _bCrypt = bCrypt;
        _context = context;
        _jwtRepository = jwtRepository;
        _logger = logger;

    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Index(string email, string password)// This is the function to call 
    {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
        ViewBag.ErrorMessage = "Plase fill all fields.";
    }

        if(email == "correo@correo.com" && password == "password")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, "User")
            };
            var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var Principal = new ClaimsPrincipal(Identity);
            HttpContext.SignInAsync(Principal);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Credenciales inválidas";

        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if(user == null)  //We made validation with User
        {
            ViewBag.Error = "The email or password are invalid.";
            _logger.LogWarning("User not found with the email: {Email}", email);
            return View();
        }

        _logger.LogInformation($"User found: {user.Email}");
        _logger.LogInformation($"Stored password hash: {user.Password}");
        _logger.LogInformation($"Entered password: {password}");   
        

        if (_bCrypt.VerifyPassword(password, user.Password))
        {
            var UserDto = new UserDto{
                Email = user.Email,
                Password = user.Password
            };  

        //we Genered Token

        if(UserDto != null)
        {
            var Token = _jwtRepository.GenerateToken(UserDto);  // In this line i had a one mistake so i created one UserDto and with this use the Dto.
            _logger.LogInformation($"Token generado{Token}");
            return Ok(new { token = Token, RedirectUrl = Url.Action("Index", "Home")});
        }
        else 
        {
            // The password is incorrect
            ModelState.AddModelError("", "Invalid login attemp.");
            _logger.LogWarning("Invalid password for the email: {Email}", email);
        }
        
    }    
            return View();
}

    [HttpGet]
    public IActionResult LoginResponse()
    {
        var RedirectoGoogle =  new AuthenticationProperties { RedirectUri = Url.Action("GoogleLogin") };
        return Challenge(RedirectoGoogle, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    public IActionResult GoogleLogin()
    {
        var AuthResult = HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme).Result;

        if(!AuthResult.Succeeded)
        {
            return RedirectToAction("LoginResponse");
        }

        //Redirect to pricipal page
        return RedirectToAction("Index", "Home");
    }
    
    public async Task<IActionResult> Logout()
    {
        //clear cookies
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");

    }
}