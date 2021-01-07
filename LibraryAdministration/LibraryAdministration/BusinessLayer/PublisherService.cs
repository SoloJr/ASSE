using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class PublisherService : BaseService<Publisher, IPublisherRepository>, IPublisherService
    {
        public PublisherService(LibraryContext context)
            : base(new PublisherRepository(context), new PublisherValidator())
        {

        }

        public ICollection<Publisher> GetAllBookPublishersOfABook(int bookId)
        {
            return _repository.GetAllBookPublishersOfABook(bookId);
        }
    }
}
