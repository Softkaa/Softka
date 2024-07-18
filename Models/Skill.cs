using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softka.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description {get; set; }
        public int CurriculumsId { get; set; }

        List<Curriculum>? Curriculums { get; set; }
    }
}