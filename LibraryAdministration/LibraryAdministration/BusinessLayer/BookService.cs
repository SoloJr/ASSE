using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    class BookService : BaseService<Book, IBookRepository>, IBookService
    {
        public BookService()
            : base(Injector.Get<IBookRepository>(), new BookValidator())
        {

        }

        public IEnumerable<Book> GetBooksWithAuthors()
        {
            return _repository.Get(
                filter: book => book.Authors.Count > 0,
                includeProperties: "Authors");
        }
    }
}
