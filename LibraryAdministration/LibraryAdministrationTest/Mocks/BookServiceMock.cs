using System.Collections.Generic;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministrationTest.Mocks
{
    internal class BookServiceMock : BaseService<Book, IBookRepository>, IBookService
    {
        public BookServiceMock()
            : base(Injector.Get<IBookRepository>(), new BookValidator())
        {

        }

        public IEnumerable<Book> GetBooksWithAuthors()
        {
            var book = new Book
            {
                Name = "name",
                Id = 1,
                Language = "ceva"
            };
            return new List<Book>{book};
        }
    }
}