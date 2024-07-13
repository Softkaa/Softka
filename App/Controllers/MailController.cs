using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softka.Services;
using Softka.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;


namespace Softka.Controllers
{
    public class MailController : Controller {
       private readonly BaseContext _context;
       private readonly MailRepository _mailRepository; 
       
       public MailController(BaseContext context, MailRepository mailRepository){
        _context = context;
        _mailRepository = mailRepository; 
       }

       public IActionResult Index(){
        return View();
       }

            [HttpPost]
       public IActionResult Recovery(string email) {

         if (string.IsNullOrWhiteSpace(email))
            {
                // Considera retornar algún mensaje de error o redirigir al usuario a una vista específica
                return View("Index");
            }
        var user = _context.Users.FirstOrDefault(e => e.Email == email);
        if(user != null){
                var subject = "Recuperación de contraseña";
                var body = $"Hola, {user.Names},\nPor favor, sigue este enlace para restablecer tu contraseña."; 
                _mailRepository.SendEmail(user.Email, subject, body, user); 
         return View("Success");
            }
            else
            {
                // Considera manejar el caso en que el correo no exista en la base de datos
                return View("Error");
            }
       }
    }
}




