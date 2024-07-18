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
using Softka.Models;

public class UserController : Controller
{
    private readonly BaseContext _context;
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository _userRepository, BaseContext context)
    {
        this._userRepository = _userRepository;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Register(User user)
    {
        System.Console.WriteLine(user);
        if (!ModelState.IsValid)
        {
            BadRequest();

            return View(user);
        }

        
        _userRepository.Add(user, user.Password);
        return RedirectToAction("Index", "Home");
    }
}