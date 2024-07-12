using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Softka.Models;

namespace Softka.Validators
{
    public class WorkExperienceValidator : AbstractValidator<WorkExperience>
    {
        public WorkExperienceValidator()
        {
            Include(new CompanyRule());
            Include(new YearRule());
        }

        public class CompanyRule : AbstractValidator<WorkExperience>
        {
            public CompanyRule()
            {
                RuleFor(company => company.Company).NotEmpty().WithMessage("Please specify a company");
            }
        }

        public class YearRule : AbstractValidator<WorkExperience>
        {
            public YearRule()
            {
                RuleFor(company => company.Year).NotNull();
            }
        }
    }
}