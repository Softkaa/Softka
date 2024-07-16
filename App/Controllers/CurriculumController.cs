using Microsoft.AspNetCore.Mvc;
using Softka.Services;
using Softka.Models;
using Softka.Infrastructure.Data;

public class CurriculumController : Controller
{
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly BaseContext _context;

    public CurriculumController(ICurriculumRepository curriculumRepository, BaseContext context)
    {
        _curriculumRepository = curriculumRepository;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }


}

