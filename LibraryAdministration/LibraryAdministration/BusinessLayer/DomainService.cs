using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    class DomainService : BaseService<Domain, IDomainRepository>, IDomainService
    {
        public DomainService()
            : base(Injector.Get<IDomainRepository>(), new DomainValidator())
        {

        }

        public IEnumerable<Domain> GetAllDomainsOfBook(int bookId)
        {
            return null;
        }
    }
}
