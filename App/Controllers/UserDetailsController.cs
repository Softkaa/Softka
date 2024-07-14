using Microsoft.AspNetCore.Mvc;
using Softka.Services;
using Softka.Models;

namespace Softka.Controllers 
{
    public class UserDetailsController : Controller
    {
    public readonly IUserRepository _userRepository;
    //In this line add the Helpers and Providers
    //private readonly HelpersUploadFiles helperUploadFiles;
    public UserDetailsController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        //_helperUploadFiles = helperUpload;
    }
    // [HttpGet]
    // public async Task <IEnumerable<User>> GetAll ()
    // {
       
    //     if(ModelState.IsValid)
    //     {
    //         await _userRepository.GetAll(); //This used when we List all
    //         return View();
    //     }
    //     else
    //     {
    //         return NotFound();
    //     }

    //}
    public IActionResult Details()
    {
        return View();
    }

    //we make the GetById
    [HttpGet("{id}")]
    public async Task <User> Details  (int id)
    {
        var User = await _userRepository.GetById(id);
         if (User == null)
         {
             return null; //This the UNIQUE that not show me Error
         }
         else 
         {
            return User;
         }
    }
    }
}

