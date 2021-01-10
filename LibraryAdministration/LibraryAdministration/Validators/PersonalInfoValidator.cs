//----------------------------------------------------------------------
// <copyright file="PersonalInfoValidator.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Validators
{
    using DomainModel;
    using FluentValidation;

    /// <summary>
    /// PersonalInfoValidator class
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{LibraryAdministration.DomainModel.PersonalInfo}" />
    public class PersonalInfoValidator : AbstractValidator<PersonalInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalInfoValidator"/> class.
        /// </summary>
        public PersonalInfoValidator()
        {
            RuleFor(x => x).NotNull().WithMessage("This should not be empty");
            RuleFor(x => x).Must(ValidatorExtension.CheckPersonalInfoNullOrEmpty).WithMessage("At least one should be specified");
        }
    }
}
