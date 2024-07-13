using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Softka.Infrastructure.Data;

namespace Softka.Controllers 
{
    public class UserDetailsController : Controller
    {
    public readonly BaseContext _context;
    //In this line add the Helpers and Providers
    //private readonly HelpersUploadFiles helperUploadFiles;
    public UserDetailsController(BaseContext context)
    {
        _context = context;
        //_helperUploadFiles = helperUpload;
    }
    public async Task <IActionResult> Details(int id)
    {
        return View (_context.Users.FirstOrDefaultAsync(u=> u.Id == id));
    }
    // [HttpGet({Id})]


    }

}