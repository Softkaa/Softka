namespace Softka.Models.Dtos
{
    public class CurriculumDto
    {
        // User
        public List<UserDto> User { get; set; } = new List<UserDto>();

        public class UserDto
        {
            public string? Names { get; set; }
            public string? LastNames { get; set; }
            public string? TypeDocument { get; set; }
            public string? Document { get; set; }
            public string? Email { get; set; }
            public string? Age { get; set; }
            public string? Password { get; set; }
        }

        // Curriculums
        public string? Nationality { get; set; }
        public string? Photo { get; set; }
        public string? Phone { get; set; }

        // Education
        public List<EducationDto> Education { get; set; } = new List<EducationDto>();

        public class EducationDto
        {
            public string? Institution { get; set; }
            public string? EducationTitle { get; set; }
        }

        // Skills
        public List<SkillDto> Skill { get; set; } = new List<SkillDto>();

        public class SkillDto
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
        }

        // WorkExperience
        public List<WorkExperienceDto> WorkExperience { get; set; } = new List<WorkExperienceDto>();

        public class WorkExperienceDto
        {
            public string? Company { get; set; }
            public DateTime Year { get; set; }
            public string Description { get; set; }
        }

        // WorkReference
        public List<WorkReferenceDto> WorkReferences { get; set; } = new List<WorkReferenceDto>();

        public class WorkReferenceDto
        {
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
        }
    }
}

