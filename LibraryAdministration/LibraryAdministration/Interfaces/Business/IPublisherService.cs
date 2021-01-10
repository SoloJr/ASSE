using LibraryAdministration.DomainModel;
using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IPublisherService : IService<Publisher>
    {
        ICollection<Publisher> GetAllBookPublishersOfABook(int bookId);
    }
}
