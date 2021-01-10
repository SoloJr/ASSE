using LibraryAdministration.DomainModel;
using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        ICollection<Publisher> GetAllBookPublishersOfABook(int bookId);
    }
}
