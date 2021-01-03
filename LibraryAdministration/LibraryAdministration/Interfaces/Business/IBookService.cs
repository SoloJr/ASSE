using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IBookService : IService<Book>
    {
        IEnumerable<Book> GetBooksWithAuthors();
    }
}
