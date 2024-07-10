using FluentValidation;
using Softka.Models;

namespace Softka.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator ()
        {
            Include(new UserNameRule());
            Include(new UserLastNameRule());
            Include(new UserPasswordRule());
            Include(new UserEmailRule());
        }

        //Start validations
        public class UserNameRule : AbstractValidator<User>
        {
            public UserNameRule()
            {
                List<string> OfensivName = new List<string>() { "Pirobo", "Gonorrea", "Ruperto" };
                RuleFor(user => user.Names).NotNull().WithMessage("The field Names is required");

                RuleFor(user => user.Names).Must(Names => !OfensivName.Contains(Names)).WithMessage("You can't put Last Name ofensive");
            }
        }

        public class UserLastNameRule : AbstractValidator<User>
        {
            public UserLastNameRule()
            {
                //List of names ofensives
                List<string> OfensiveLastNames = new List<string>() { "Popo", "Caca", "Idiota", "Fuck", "Gay" };
                RuleFor(user => user.LastNames).NotNull().WithMessage("The field LastName is required and can't be null or empty");

                RuleFor(user => user.LastNames).Must(LastNames => !OfensiveLastNames.Contains(LastNames)).WithMessage("You can't put Last Name ofensive");
            }
        }

        public class UserPasswordRule : AbstractValidator<User>
        {
            public UserPasswordRule()
            {
                RuleFor(user => user.Password).NotNull().WithMessage("The field Password can't be null or empty");
            }
        }

        public class UserEmailRule : AbstractValidator<User>
        {
            public UserEmailRule()
            {
                RuleFor(user => user.Email).NotNull().WithMessage("The Email must have the characteristics as email addres");

                RuleFor(user => user.Email).EmailAddress();
            }
        }
    }
}