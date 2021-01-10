//----------------------------------------------------------------------
// <copyright file="EmployeeValidator.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Validators
{
    using FluentValidation;
    using LibraryAdministration.DomainModel;

    /// <summary>
    /// EmployeeValidator class
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{LibraryAdministration.DomainModel.Employee}" />
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeValidator"/> class.
        /// </summary>
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.Address).NotEmpty().MinimumLength(3).MaximumLength(100);
        }
    }
}
