using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Softka.Models.DTOs;

namespace Softka.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            Include(new EmailRule());
            Include(new PasswordRule());
        }

        public class EmailRule : AbstractValidator<UserDto>
        {
            public EmailRule()
            {
                RuleFor(email => email.Email).NotNull().WithMessage("Please specify the email");
                RuleFor(email => email.Email).EmailAddress();
                RuleFor(email => email.Email).Length(20, 250);
            }
        }

        public class PasswordRule : AbstractValidator<UserDto>
        {
            public PasswordRule()
            {
                RuleFor(email => email.Password).NotEmpty().WithMessage("The fiel is required");
            }
        }
    }
}