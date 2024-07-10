using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softka.Models
{
    public class Authentication
    {
        public int Id { get; set; }
        public string? Jwt { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
    }
}