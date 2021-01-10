//----------------------------------------------------------------------
// <copyright file="AuthorValidator.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Validators
{
    using System;
    using DomainModel;
    using FluentValidation;

    /// <summary>
    /// AuthorValidator class
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{LibraryAdministration.DomainModel.Author}" />
    public class AuthorValidator : AbstractValidator<Author>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorValidator"/> class.
        /// </summary>
        public AuthorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
            RuleFor(x => x.BirthDate).NotNull();
            RuleFor(x => x.BirthDate).Must(x => x.Date > DateTime.MinValue);
            RuleFor(x => x.Country).NotEmpty();
        }
    }
}
