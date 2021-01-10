using FluentValidation;
using LibraryAdministration.DomainModel;
using System;

namespace LibraryAdministration.Validators
{
    public class PublisherValidator : AbstractValidator<Publisher>
    {
        public PublisherValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(30);
            RuleFor(x => x.Headquarter).NotEmpty().MinimumLength(3).MaximumLength(30);
            RuleFor(x => x.FoundingDate).Must(x => x > DateTime.MinValue);
        }
    }
}
