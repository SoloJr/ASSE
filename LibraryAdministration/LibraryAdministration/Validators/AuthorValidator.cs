using FluentValidation;
using LibraryAdministration.DomainModel;
using System;

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
