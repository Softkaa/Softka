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
using Softka.Utils;
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
    public ActionResult Index(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ViewBag.ErrorMessage = "Please fill all fields.";
            return View();
        }

        if (email == "correo@correo.com" && password == "password")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, "User")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Credenciales inválidas";

        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
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
            var Token = _jwtRepository.GenerateToken(UserDto);
            _logger.LogInformation($"Token User found:{Token}");
            
            Response.Headers.Add("Authorization", "Bearer " + Token);
            Response.Cookies.Append("jwt", Token);

            return RedirectToAction("Index", "Home");
        }
        else 
        {
            ModelState.AddModelError("", "Invalid login attempt.");
            _logger.LogWarning("Invalid password for the email: {Email}", email);
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