using Softka.Models;


namespace Softka.Services
{
    public interface ICurriculumRepository
    {
        Task<Curriculum> GetCurriculum(int id);
        Task<IEnumerable<Curriculum>> GetCurriculums();
        Task<Curriculum> CreateCurriculum(Curriculum curriculum);
        Task<Curriculum> UpdateCurriculum(int id, Curriculum curriculum);
        Task<bool> DeleteCurriculum(int id);
    }
}