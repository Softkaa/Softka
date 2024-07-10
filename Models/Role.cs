using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softka.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //objecto of UserRoles
        public UserRole? userRole { get; set; }
    }
}