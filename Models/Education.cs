using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softka.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string? Institution { get; set; }
        public string? EducationaTitle { get; set; }

        List<Curriculum>? Curriculums { get; set; }
    }
}