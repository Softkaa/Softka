using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Softka.Models;

namespace Softka.Validators
{
    public class CurriculumValidator : AbstractValidator<Curriculum>
    {
        public CurriculumValidator()
        {
            Include(new AgeRule());
            Include(new NationalityRule());
            Include(new PhotoRule());
            Include(new PhoneRule());
        }

        public class AgeRule : AbstractValidator<Curriculum>
        {
            public AgeRule()
            {
                RuleFor(age => age.Age).NotNull().WithMessage("Please add an age");
            }
        }

        public class NationalityRule : AbstractValidator<Curriculum>
        {
            public NationalityRule()
            {
                RuleFor(nat => nat.Nationality).NotEmpty().WithMessage("This fiel is required");
            }
        }

        public class PhotoRule : AbstractValidator<Curriculum>
        {
            public PhotoRule()
            {
                RuleFor(photo => photo.Photo)
                                            .NotEmpty()
                                            .WithMessage("The photo is required");
            }
        }

        public class PhoneRule : AbstractValidator<Curriculum>
        {
            public PhoneRule()
            {
                RuleFor(phone => phone.Phone).NotNull().WithMessage("The fiel is required");
            }
        }
    }
}