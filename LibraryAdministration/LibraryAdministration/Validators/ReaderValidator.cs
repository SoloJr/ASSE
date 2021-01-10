//----------------------------------------------------------------------
// <copyright file="ReaderValidator.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Validators
{
    using FluentValidation;
    using LibraryAdministration.DomainModel;

    /// <summary>
    /// ReaderValidator class
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{LibraryAdministration.DomainModel.Reader}" />
    public class ReaderValidator : AbstractValidator<Reader>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderValidator"/> class.
        /// </summary>
        public ReaderValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.Address).NotEmpty().MinimumLength(3).MaximumLength(100);
        }
    }
}
