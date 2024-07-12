using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Softka.Utils.PasswordHashing;
using Softka.Infrastructure.Data;
using Softka.Services;
using Softka.Models.DTOs;
using Softka.Models;

public class AccountController : Controller
{
    private readonly Bcrypt _bCrypt;
    private readonly BaseContext _context;
    private readonly IJwtRepository _jwtRepository;

    public AccountController(Bcrypt bCrypt, BaseContext context, IJwtRepository jwtRepository)
    {
        _bCrypt = bCrypt;
        _context = context;
        _jwtRepository = jwtRepository;
    }

    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user != null && _bCrypt.VerifyPassword(password, user.Password))
        {
            var UserDto = new UserDto{
                Email = user.Email,
                Password = user.Password
            };
            //we Genered Token
            var Token = _jwtRepository.GenerateToken(UserDto);  // In this line i had a one mistake so i created one UserDto and with this use the Dto.
                       
            return Ok(new { token = Token, RedirectUrl = Url.Action("Index", "Home")});
        }
        else 
        {
            // The password is incorrect
            ModelState.AddModelError("", "Invalid login attemp.");
            return View();
        }
    }

    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            // Hash the password
            user.Password = _bCrypt.HashPassword(user.Password);

            // Add the user to the database
            _bCrypt.CreateUser(user);

            return RedirectToAction("Index");
        }

        // The model is invalid
        return View(user);
    }
}