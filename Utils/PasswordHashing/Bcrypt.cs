using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using Softka.Infrastructure.Data;
using Softka.Models;
=======
>>>>>>> google-login

namespace Softka.Utils.PasswordHashing
{
    public class Bcrypt
    {
<<<<<<< HEAD
        private readonly BaseContext _context;

        public Bcrypt(BaseContext context)
        {
            _context = context;
        }

=======
>>>>>>> google-login
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
<<<<<<< HEAD

        public void CreateUser(User user)
        {
            // Add the object to db
            _context.Users.Add(user);
            _context.SaveChanges();
        }
=======
>>>>>>> google-login
    }
}