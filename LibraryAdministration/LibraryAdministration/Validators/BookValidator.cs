using FluentValidation;
using LibraryAdministration.DomainModel;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace LibraryAdministration.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        private readonly string _dom = ConfigurationManager.AppSettings["DOM"];

        public BookValidator()
        {
            var dom = int.Parse(_dom);
            RuleFor(book => book.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
            RuleFor(book => book.Language).NotEmpty().MinimumLength(3).MaximumLength(20);
            RuleFor(book => book.Year).NotEmpty();
            RuleFor(book => book.Domains).Must(x => x.Count <= dom)
                .WithMessage($"The book cannot be in more than {dom} domains");
            //RuleFor(book => book.Domains).Must(RuleForNumberOfDomains).WithMessage("Too many domains");
            RuleFor(book => book.Authors).Must(RuleForAuthors).WithMessage("Specify authors");
        }

        private bool RuleForAuthors(ICollection<Author> authors)
        {
            if (authors == null)
            {
                return false;
            }

            if (authors.Count == 0)
            {
                return false;
            }

            return true;
        }

        private bool RuleForNumberOfDomains(ICollection<Domain> domains)
        {
            var dom = int.Parse(_dom);
            var count = domains.Count(d => d.EntireDomainId == null);

            if (count > dom)
            {
                return false;
            }

            return count <= dom;
        }

        //private bool RuleForMultipleDomainsOfSameKind(ICollection<Domain> domains)
        //{
        //    var orderedDomains = domains.ToList().OrderBy(x => x.ParentId).ToList();


        //}
    }
}
