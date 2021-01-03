using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    class BookPublisherValidator : AbstractValidator<BookPublisher>
    {
        public BookPublisherValidator()
        {
            
        }
    }
}
