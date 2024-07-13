using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Softka.Infrastructure.Data;
using Softka.Models;

namespace Softka.Services
{
    public class UserService
    {
        private readonly IValidator<User> _userValidator;
        private readonly BaseContext _context;
        public UserService(IValidator<User> userValidator, BaseContext context)
        {
            _userValidator = userValidator;
            _context = context;
        }

        public async Task AddUser(User user)
        {
            var Result = _userValidator.Validate(user);

            if(!Result.IsValid)
            {
                foreach (var error in Result.Errors)
                {
                    Console.WriteLine($"Property: {error.PropertyName} Error: {error.ErrorMessage}");
                }
            }
            else
            {
                //save user in context
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}