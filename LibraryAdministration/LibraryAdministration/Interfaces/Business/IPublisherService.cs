using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IPublisherService : IService<Publisher>
    {
        ICollection<Publisher> GetAllBookPublishersOfABook(int bookId);
    }
}
