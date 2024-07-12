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
    private readonly Bcrypt _bcrypt;
    private readonly BaseContext _context;

    public UserController(Bcrypt bcrypt, BaseContext context)
    {
        _bcrypt = bcrypt;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            // Hash the password
            user.Password = _bcrypt.HashPassword(user.Password);

            // Add the user to the database
            _bcrypt.CreateUser(user);

            return RedirectToAction("Index");
        }

        // The model is invalid
        return View(user);
    }
}