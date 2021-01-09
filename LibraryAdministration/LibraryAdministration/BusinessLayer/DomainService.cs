using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class DomainService : BaseService<Domain, IDomainRepository>, IDomainService
    {
        public DomainService(LibraryContext context)
            : base(new DomainRepository(context), new DomainValidator())
        {

        }

        public IEnumerable<Domain> GetAllParentDomains(int domainId)
        {
            if (domainId <= 0)
            {
                throw new LibraryArgumentException(nameof(domainId));
            }

            return _repository.GetAllParentDomains(domainId);
        }


        public bool CheckDomainConstraint(List<Domain> domains)
        {
            if (domains == null)
            {
                throw new LibraryArgumentException(nameof(domains));
            }

            return _repository.CheckDomainConstraint(domains);
        }
    }
}
