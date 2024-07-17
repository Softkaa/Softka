using Microsoft.AspNetCore.Mvc;
using Softka.Services;
using Softka.Models;
using Softka.Models.Dtos;

namespace Softka.Controllers 
{
    public class CurriculumController : Controller
    {
        public readonly ICurriculumRepository _curriculumRepository;
        // In this line add the Helpers and Providers
        // private readonly HelpersUploadFiles helperUploadFiles;
        public CurriculumController(ICurriculumRepository curriculumRepository)
        {
            _curriculumRepository = curriculumRepository;
            //_helperUploadFiles = helperUpload; 
        }
        public IActionResult UserInformation()
        {
            var CurriculumDto = _curriculumRepository.GetCurriculum();
            if(CurriculumDto == null)
            {
                return NotFound();
            }            
            return View(CurriculumDto);
        }
    }
}



