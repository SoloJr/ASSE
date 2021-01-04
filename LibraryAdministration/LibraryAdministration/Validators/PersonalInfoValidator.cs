using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    public class PersonalInfoValidator : AbstractValidator<PersonalInfo>
    {
        public PersonalInfoValidator()
        {
            RuleFor(x => x).NotNull().WithMessage("This should not be empty");
            RuleFor(x => x).Must(ValidatorExtension.CheckPersonalInfoNullOrEmpty).WithMessage("At least one should be specified");
        }
    }
}
