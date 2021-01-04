using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.DataAccess;

namespace LibraryAdministrationTest.Mocks
{
    class ReaderRepositoryMock : BaseRepositoryMock<Reader>, IReaderRepository
    {
    }
}
