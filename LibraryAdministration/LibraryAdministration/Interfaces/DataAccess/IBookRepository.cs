using LibraryAdministration.DomainModel;
using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Domain> GetAllDomainsOfBook(int bookId);
    }
}
