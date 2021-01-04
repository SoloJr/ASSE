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
    class ReaderServiceMock : BaseService<Reader, IReaderRepository>, IReaderService
    {
        public ReaderServiceMock()
            : base(Injector.Get<IReaderRepository>(), new ReaderValidator())
        {

        }
    }
}
