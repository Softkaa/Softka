using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Softka.Models;

namespace Softka.Validators
{
    public class WorkReferenceValidator : AbstractValidator<WorkReference>
    {
        public WorkReferenceValidator()
        {
            Include(new NameRule());
            Include(new EmailRule());
            Include(new PhoneRule());
        }

        public class NameRule : AbstractValidator<WorkReference>
        {
            public NameRule()
            {
                RuleFor(wr => wr.Name).NotEmpty().WithMessage("Please specify company name");
            }
        }

        public class EmailRule : AbstractValidator<WorkReference>
        {
            public EmailRule()
            {
                RuleFor(wr => wr.Email).NotNull().WithMessage("The email field is required");
                RuleFor(wr => wr.Email).Length(20, 100).EmailAddress();
            }
        }

        public class PhoneRule : AbstractValidator<WorkReference>
        {
            public PhoneRule()
            {
                RuleFor(wr => wr.Phone).NotEmpty().WithMessage("The phone field is required");
            }
        }
    }
}