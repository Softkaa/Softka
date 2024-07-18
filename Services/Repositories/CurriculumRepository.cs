using Microsoft.EntityFrameworkCore;
using Softka.Infrastructure.Data;
using Softka.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softka.Services.Repositories
{
    public class CurriculumRepository : ICurriculumRepository
    {
        private readonly BaseContext _context;

        public CurriculumRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<Curriculum> GetCurriculum(int id)
        {
            var curriculum = await _context.Curriculums
                .Include(c => c.Skill)
                .Include(c => c.Education)
                .Include(c => c.WorkExperience)
                .Include(c => c.WorkReference)
                .FirstOrDefaultAsync(c => c.Id == id);
            return curriculum;
        }

        public async Task<IEnumerable<Curriculum>> GetCurriculums()
        {
            var curriculums = await _context.Curriculums
                .Include(c => c.Skill)
                .Include(c => c.Education)
                .Include(c => c.WorkExperience)
                .Include(c => c.WorkReference)
                .ToListAsync();
            return curriculums;
        }

        public async Task<Curriculum> CreateCurriculum(Curriculum curriculum)
        {
            _context.Curriculums.Add(curriculum);
            await _context.SaveChangesAsync();
            return curriculum;
        }

        public async Task<Curriculum> UpdateCurriculum(int id, Curriculum updatedCurriculum)
        {
            var curriculum = await _context.Curriculums.FirstOrDefaultAsync(c => c.Id == id);
            if (curriculum == null)
            {
                return null;
            }

            curriculum.Age = updatedCurriculum.Age;
            curriculum.Nationality = updatedCurriculum.Nationality;
            curriculum.Photo = updatedCurriculum.Photo;
            curriculum.Phone = updatedCurriculum.Phone;
            curriculum.UserId = updatedCurriculum.UserId;
            curriculum.Skill = updatedCurriculum.Skill;
            curriculum.Education = updatedCurriculum.Education;
            curriculum.WorkExperience = updatedCurriculum.WorkExperience;
            curriculum.WorkReference = updatedCurriculum.WorkReference;

            await _context.SaveChangesAsync();
            return curriculum;
        }

        public async Task<bool> DeleteCurriculum(int id)
        {
            var curriculum = await _context.Curriculums.FirstOrDefaultAsync(c => c.Id == id);
            if (curriculum == null)
            {
                return false;
            }
            _context.Curriculums.Remove(curriculum);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}