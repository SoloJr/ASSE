using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    public class ReaderBookValidator : AbstractValidator<ReaderBook>
    {
        public ReaderBookValidator()
        {
            RuleFor(x => x.DueDate).Must(x => x > DateTime.MinValue);
            RuleFor(x => x.LoanDate).Must(x => x > DateTime.MinValue);
            RuleFor(x => x.BookPublisherId).NotEmpty();
            RuleFor(x => x.ReaderId).NotEmpty();
        }
    }
}
