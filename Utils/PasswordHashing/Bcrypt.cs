using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softka.Infrastructure.Data;
using Softka.Models;

namespace Softka.Utils.PasswordHashing
{
    public class Bcrypt
    {
        private readonly BaseContext _context;

        public Bcrypt(BaseContext context)
        {
            _context = context;
        }
        public string HashPassword(string password)
        {   
            // Generate the password hash
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Verify the password against the hash
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        /* public void CreateUser(User user)
        {
            // Add the object to db
            user.DateRegister = DateTime.Now;
            _context.Users.Add(user);
            _context.SaveChanges();
        } */
    }
}