using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Validators;
using System.Collections.Generic;

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
            if (bookId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookId));
            }

            return _repository.GetAllBookPublishersOfABook(bookId);
        }
    }
}
