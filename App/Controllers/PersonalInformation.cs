using Microsoft.AspNetCore.Mvc;
using Softka.Services;
using Softka.Models;
using Softka.Models.Dtos;

namespace Softka.Controllers 
{
    public class PersonalInformationController : Controller
    {
        public readonly IPersonalInformationRepository _personalInformationRepository;
        // In this line add the Helpers and Providers
        // private readonly HelpersUploadFiles helperUploadFiles;
        public PersonalInformationController(IPersonalInformationRepository personalInformationRepository)
        {
            _personalInformationRepository = personalInformationRepository;
            //_helperUploadFiles = helperUpload; 
        }
        public IActionResult PersonalInformation()
        {
            var CurriculumDto = _personalInformationRepository.GetCurriculum();
            if(CurriculumDto == null)
            {
                return NotFound(Utils.Exceptions.ErrorExceptions.CreateNotFound());
            }            
            return View(CurriculumDto);
        }
    }
}



