//----------------------------------------------------------------------
// <copyright file="BookValidator.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Validators
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using DomainModel;
    using FluentValidation;

    /// <summary>
    /// BookValidator class
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{LibraryAdministration.DomainModel.Book}" />
    public class BookValidator : AbstractValidator<Book>
    {
        /// <summary>
        /// The DOM
        /// </summary>
        private readonly string domain = ConfigurationManager.AppSettings["DOM"];

        /// <summary>
        /// Initializes a new instance of the <see cref="BookValidator"/> class.
        /// </summary>
        public BookValidator()
        {
            var dom = int.Parse(this.domain);
            RuleFor(book => book.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
            RuleFor(book => book.Language).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(book => book.Year).NotEmpty();
            RuleFor(book => book.Domains).Must(x => x.Count <= dom)
                .WithMessage($"The book cannot be in more than {dom} domains");
            RuleFor(book => book.Authors).Must(this.RuleForAuthors).WithMessage("Specify authors");
        }

        /// <summary>
        /// Rules for authors.
        /// </summary>
        /// <param name="authors">The authors.</param>
        /// <returns>boolean value</returns>
        private bool RuleForAuthors(ICollection<Author> authors)
        {
            if (authors == null)
            {
                return false;
            }

            return authors.Count != 0;
        }
    }
}
