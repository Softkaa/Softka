using Softka.Models.Dtos;

namespace Softka.Services
{
    public interface IPersonalInformationRepository
    {
        CurriculumDto GetCurriculum();
    }
}