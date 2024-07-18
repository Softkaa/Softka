using System.Linq;
using EntityFrameworkCoreJwtTokenAuth.Interfaces;
using Microsoft.EntityFrameworkCore;
using Softka.Infrastructure.Data;
using Softka.Models;
using Softka.Models.Dtos;
using Softka.Services;
namespace Softka.Repositories
{
    public class PersonalInformationRepository : IPersonalInformationRepository
    {
        public CurriculumDto GetCurriculum()
        {
            return new CurriculumDto
            {
                Nationality = "Colombiana",
                Photo = "/images/photo.jpg",
                Phone = "123456789",
                User = new List<CurriculumDto.UserDto>
                {
                    new CurriculumDto.UserDto
                    {
                        Names = "Juan",
                        LastNames = "Perez",
                        TypeDocument = "CC",
                        Document = "123456789",
                        Email = "juan.perez@example.com",
                        Age = "30",
                        Password = "password1"
                    }
                },
                Education = new List<CurriculumDto.EducationDto>
                {
                    new CurriculumDto.EducationDto
                    {
                        Institution = "Universidad Nacional",
                        EducationTitle = "Ingeniería de Sistemas"
                    }
                },
                Skill = new List<CurriculumDto.SkillDto>
                {
                    new CurriculumDto.SkillDto
                    {
                        Name = "Programación",
                        Description = "Desarrollo de software en C# y ASP.NET"
                    }
                },
                WorkExperience = new List<CurriculumDto.WorkExperienceDto>
                {
                    new CurriculumDto.WorkExperienceDto
                    {
                        Company = "Softka",
                        Year = new DateTime(2020, 1, 1),
                        Description = "Desarrollador de software"
                    }
                },
                WorkReferences = new List<CurriculumDto.WorkReferenceDto>
                {
                    new CurriculumDto.WorkReferenceDto
                    {
                        Name = "Mateo",
                        Email = "Mateo@gmail.com",
                        Phone = "987654321"
                    }
                }
            };
        }
    }
}

