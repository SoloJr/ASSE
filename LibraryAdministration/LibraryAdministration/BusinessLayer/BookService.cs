using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class BookService : BaseService<Book, IBookRepository>, IBookService
    {
        public BookService(LibraryContext context)
            : base(new BookRepository(context), new BookValidator())
        {

        }

        public IEnumerable<Book> GetBooksWithAuthors()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Domain> GetAllDomainsOfBook(int bookId)
        {
            return _repository.GetAllDomainsOfBook(bookId);
        }
    }
}
