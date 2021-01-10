﻿using FluentValidation;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    public class DomainValidator : AbstractValidator<Domain>
    {
        public DomainValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(30); ;
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
