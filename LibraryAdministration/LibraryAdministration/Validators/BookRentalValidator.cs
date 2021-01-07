using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    public class BookRentalValidator : AbstractValidator<BookRental>
    {
        public BookRentalValidator()
        {
            RuleFor(x => x.ForRent).NotEmpty();
            RuleFor(x => x.RentBookPublisherId).NotEmpty();
        }
    }
}
