using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IBookPublisherService : IService<BookPublisher>
    {
        IEnumerable<BookPublisher> GetAllEditionsOfBook(int bookId);

        bool CheckBookDetailsForAvailability(int bookPublisherId);
    }
}
