using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Validators;
using System.Collections.Generic;

namespace LibraryAdministration.BusinessLayer
{
    public class BookService : BaseService<Book, IBookRepository>, IBookService
    {
        public BookService(LibraryContext context)
            : base(new BookRepository(context), new BookValidator())
        {

        }

        public IEnumerable<Domain> GetAllDomainsOfBook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookId));
            }

            return _repository.GetAllDomainsOfBook(bookId);
        }
    }
}
