using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(x => x.Address).NotEmpty().MinimumLength(3).MaximumLength(100);
        }
    }
}
