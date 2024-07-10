using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softka.Models
{
    public class User
    {
        
        public int Id { get; set; }
        public string? Names { get; set; }
        public string? LastNames { get; set; }
        public string? Document { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string? Password { get; set; }
        public DateTime DateRegister { get; set; }

        //Object of Role
        public UserRole? UserRole { get; set; }
        public Curriculum? Curriculum { get; set; }
        public Authentication? Authentication { get; set; }
    }
}