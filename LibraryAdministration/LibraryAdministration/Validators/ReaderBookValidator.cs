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
            RuleFor(x => x.LoanDate).NotEmpty();
            RuleFor(x => x.BookId).NotEmpty();
            RuleFor(x => x.ReaderId).NotEmpty();
        }
    }
}
