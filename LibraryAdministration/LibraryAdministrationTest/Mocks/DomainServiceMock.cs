using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministrationTest.Mocks
{
    class DomainServiceMock : BaseService<Domain, IDomainRepository>, IDomainService
    {
        public DomainServiceMock()
            : base(Injector.Get<IDomainRepository>(), new DomainValidator())
        {

        }

        public IEnumerable<Domain> GetAllDomainsOfBook(int bookId)
        {
            throw new NotImplementedException();
        }
    }
}
