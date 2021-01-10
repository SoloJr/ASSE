//----------------------------------------------------------------------
// <copyright file="DomainValidator.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Validators
{
    using DomainModel;
    using FluentValidation;

    /// <summary>
    /// DomainValidator class
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{LibraryAdministration.DomainModel.Domain}" />
    public class DomainValidator : AbstractValidator<Domain>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainValidator"/> class.
        /// </summary>
        public DomainValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(30);
            RuleFor(x => x).Must(this.ParentTesterDomain).WithMessage("You have to specify the parent if the domain is set");
        }

        /// <summary>
        /// Parents the tester domain.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>boolean value</returns>
        private bool ParentTesterDomain(Domain d)
        {
            if (d.ParentId != null)
            {
                return d.EntireDomainId != null;
            }

            return d.EntireDomainId == null;
        }
    }
}
