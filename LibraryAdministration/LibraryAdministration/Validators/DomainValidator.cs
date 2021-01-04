using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    public class DomainValidator : AbstractValidator<Domain>
    {
        public DomainValidator()
        {
            RuleFor(x => x).Must(ParentTesterDomain).WithMessage("You have to specify the parent if the domain is set");
        }

        private bool ParentTesterDomain(Domain d)
        {
            if (d.ParentId != null)
            {
                return d.EntireDomainId != null;
            }

            return d.EntireDomainId == null;
        }
    }
}
