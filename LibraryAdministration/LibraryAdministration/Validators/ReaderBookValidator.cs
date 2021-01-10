//----------------------------------------------------------------------
// <copyright file="ReaderBookValidator.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Validators
{
    using System;
    using DomainModel;
    using FluentValidation;

    /// <summary>
    /// ReaderBookValidator class
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{LibraryAdministration.DomainModel.ReaderBook}" />
    public class ReaderBookValidator : AbstractValidator<ReaderBook>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderBookValidator"/> class.
        /// </summary>
        public ReaderBookValidator()
        {
            RuleFor(x => x).Must(this.CheckLoanDate);
            RuleFor(x => x.LoanDate).Must(x => x > DateTime.MinValue);
            RuleFor(x => x.BookPublisherId).NotEmpty();
            RuleFor(x => x.ReaderId).NotEmpty();
            RuleFor(x => x.ExtensionDays).Must(x => x == 0).WithMessage("By default should be zero");
        }

        /// <summary>
        /// Checks the loan date.
        /// </summary>
        /// <param name="rb">The reader book</param>
        /// <returns>boolean value</returns>
        private bool CheckLoanDate(ReaderBook rb)
        {
            var loanDate = new DateTime(rb.LoanDate.Year, rb.LoanDate.Month, rb.LoanDate.Day);
            var dueDateNew = rb.DueDate.AddDays(-14);
            var dueDate = new DateTime(dueDateNew.Year, dueDateNew.Month, dueDateNew.Day);

            return loanDate == dueDate;
        }
    }
}
