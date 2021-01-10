using FluentValidation;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    public class ReaderValidator : AbstractValidator<Reader>
    {
        public ReaderValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.Address).NotEmpty().MinimumLength(3).MaximumLength(100);
        }
    }
}
