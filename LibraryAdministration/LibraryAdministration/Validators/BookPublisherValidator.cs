//----------------------------------------------------------------------
// <copyright file="BookPublisherValidator.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Validators
{
    using System;
    using DomainModel;
    using FluentValidation;

    /// <summary>
    /// BookPublisherValidator class
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{LibraryAdministration.DomainModel.BookPublisher}" />
    public class BookPublisherValidator : AbstractValidator<BookPublisher>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookPublisherValidator"/> class.
        /// </summary>
        public BookPublisherValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.PublisherId).NotEmpty();
            RuleFor(x => x.RentCount).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Pages).NotEmpty();
            RuleFor(x => x.ReleaseDate).Must(x => x > DateTime.MinValue);
            RuleFor(x => x.ForLecture).NotEmpty();
            RuleFor(x => x.ForRent).NotEmpty();
        }
    }
}
