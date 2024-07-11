using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Softka.Models;
using Softka.Utils.PasswordHashing;
using Softka.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

public class LoginController : Controller
{
    private readonly Bcrypt _bCrypt;
    private readonly BaseContext _context;

    public LoginController(Bcrypt bCrypt, BaseContext context)
    {
        _bCrypt = bCrypt;
        _context = context;
    }

    public IActionResult Index()
    {
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

    [HttpPost]
    public async Task<ActionResult> Index(string email, string password)
    {

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

        //Bcrypt
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null && _bCrypt.VerifyPassword(password, user.Password))
        {
            // The password is success
            // Here you can manage the login logic
            return RedirectToAction("Index", "Home");
        }

        // The password is incorrect
        ModelState.AddModelError("", "Invalid login attemp.");
        return View();
    }

    public IActionResult Logout()
    {
        //clear cookies
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }
}