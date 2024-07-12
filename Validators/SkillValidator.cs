using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Softka.Models;

namespace Softka.Validators
{
    public class SkillValidator : AbstractValidator<Skill>
    {
        public SkillValidator()
        {
            Include(new NameRule());
        }

        public class NameRule : AbstractValidator<Skill>
        {
            public NameRule()
            {
                RuleFor(skill => skill.Name).NotEmpty().WithMessage("Please write skill");
            }
        }
    }
}