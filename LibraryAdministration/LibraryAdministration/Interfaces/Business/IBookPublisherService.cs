using LibraryAdministration.DomainModel;
using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IBookPublisherService : IService<BookPublisher>
    {
        IEnumerable<BookPublisher> GetAllEditionsOfBook(int bookId);

        bool CheckBookDetailsForAvailability(int bookPublisherId);
    }
}
