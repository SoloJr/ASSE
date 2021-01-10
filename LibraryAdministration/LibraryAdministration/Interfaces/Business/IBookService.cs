using LibraryAdministration.DomainModel;
using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IBookService : IService<Book>
    {
        IEnumerable<Domain> GetAllDomainsOfBook(int bookId);
    }
}
