using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softka.Models
{
    public class Authentication
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsRefreshed { get; set; }
        public int UserId { get; set; }
    }
}