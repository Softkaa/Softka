using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}