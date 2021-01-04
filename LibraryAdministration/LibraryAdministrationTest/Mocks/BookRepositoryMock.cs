using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.DataAccess;

namespace LibraryAdministrationTest.Mocks
{
    internal class BookRepositoryMock : BaseRepositoryMock<Book>, IBookRepository
    {

    }
}