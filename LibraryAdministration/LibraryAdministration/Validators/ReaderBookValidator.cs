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
            RuleFor(x => x ).Must(CheckLoanDate);
            RuleFor(x => x.LoanDate).Must(x => x > DateTime.MinValue);
            RuleFor(x => x.BookPublisherId).NotEmpty();
            RuleFor(x => x.ReaderId).NotEmpty();
            RuleFor(x => x.ExtensionDays).Must(x => x == 0).WithMessage("By default should be zero");
        }

        private bool CheckLoanDate(ReaderBook rb)
        {
            var loanDate = new DateTime(rb.LoanDate.Year, rb.LoanDate.Month, rb.LoanDate.Day);
            var dueDateNew = rb.DueDate.AddDays(-14);
            var dueDate = new DateTime(dueDateNew.Year, dueDateNew.Month, dueDateNew.Day);

            return loanDate == dueDate;
        }
    }
}
