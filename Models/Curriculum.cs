using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softka.Models
{
    public class Curriculum
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string? Nationality { get; set; }
        public string? Photo { get; set; }
        public string? Phone { get; set; }
        public int UserId { get; set; }


        //Navigation to User
        List<User>? Users { get; set; }

        //objects of others tanles
        public Skill? Skill { get; set; }
        public Education? Education { get; set; }
        public WorkExperience? WorkExperience { get; set; }
        public WorkReference? WorkReference { get; set; }
        
    }
}