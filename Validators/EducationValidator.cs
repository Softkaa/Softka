using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Softka.Models;

namespace Softka.Validators
{
    public class EducationValidator : AbstractValidator<Education>
    {   
        public EducationValidator()
        {
            Include(new InstitutionRule());
            Include(new EducationTitleRule());
        }

        public class InstitutionRule : AbstractValidator<Education>
        {
            public InstitutionRule()
            {
                RuleFor(ins => ins.Institution).NotEmpty().WithMessage("The Institution is required");
            }
        }

        public class EducationTitleRule : AbstractValidator<Education>
        {
            public EducationTitleRule()
            {
                RuleFor(edu => edu.EducationaTitle).NotEmpty().WithMessage("Please write an education title");
                RuleFor(edu => edu.EducationaTitle).Length(20, 100);
            }
        }
    }
}