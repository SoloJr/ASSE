using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
            RuleFor(x => x.BirthDate).NotNull();
            RuleFor(x => x.BirthDate).Must(x => x.Date > DateTime.MinValue);
            RuleFor(x => x.Country).NotEmpty();
        }
    }
}
