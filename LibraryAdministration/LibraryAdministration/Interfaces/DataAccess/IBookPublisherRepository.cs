using LibraryAdministration.DomainModel;
using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IBookPublisherRepository : IRepository<BookPublisher>
    {
        IEnumerable<BookPublisher> GetAllEditionsOfBook(int bookId);

        bool CheckBookDetailsForAvailability(int bookPublisherId);
    }
}
