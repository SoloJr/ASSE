using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    public class BookPublisherValidator : AbstractValidator<BookPublisher>
    {
        public BookPublisherValidator()
        {
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.PublisherId).NotEmpty();
            RuleFor(x => x.RentCount).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Pages).NotEmpty();
            RuleFor(x => x.ReleaseDate).Must(x => x > DateTime.MinValue);
        }
    }
}
