//----------------------------------------------------------------------
// <copyright file="PublisherValidator.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Validators
{
    using System;
    using DomainModel;
    using FluentValidation;

    /// <summary>
    /// PublisherValidator class
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{LibraryAdministration.DomainModel.Publisher}" />
    public class PublisherValidator : AbstractValidator<Publisher>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherValidator"/> class.
        /// </summary>
        public PublisherValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(30);
            RuleFor(x => x.Headquarter).NotEmpty().MinimumLength(3).MaximumLength(30);
            RuleFor(x => x.FoundingDate).Must(x => x > DateTime.MinValue);
        }
    }
}
