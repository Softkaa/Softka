using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Softka.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        //Navigation to other tables
        [JsonIgnore]
        List<User>? Users { get; set; }
        [JsonIgnore]
        List<Role>? Roles { get; set; }
    }
}