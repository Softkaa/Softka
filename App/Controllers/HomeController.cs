using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Softka.Utils;

namespace Softka.App.Controllers
{
    [AuthorizationRequired]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private class AuthorizationRequiredAttribute : Attribute
        {
        }
    }

}