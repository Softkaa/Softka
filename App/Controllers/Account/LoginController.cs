using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Softka.Models;
using Softka.Utils.PasswordHashing;
using Softka.Infrastructure.Data;

public class AccountController : Controller
{
    private readonly Bcrypt _bCrypt;
    private readonly BaseContext _context;

    public AccountController(Bcrypt bCrypt, BaseContext context)
    {
        _bCrypt = bCrypt;
        _context = context;
    }

    [HttpPost]
    public ActionResult Login(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
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
}