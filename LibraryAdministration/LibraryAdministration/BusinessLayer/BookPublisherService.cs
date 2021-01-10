using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class BookPublisherService : BaseService<BookPublisher, IBookPublisherRepository>, IBookPublisherService
    {
        public BookPublisherService(LibraryContext context)
            : base(new BookPublisherRepository(context), new BookPublisherValidator())
        {
            
        }

        public IEnumerable<BookPublisher> GetAllEditionsOfBook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookId));
            }

            return _repository.GetAllEditionsOfBook(bookId);
        }

        public bool CheckBookDetailsForAvailability(int bookPublisherId)
        {
            if (bookPublisherId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookPublisherId));
            }

            return _repository.CheckBookDetailsForAvailability(bookPublisherId);
        }
    }
}
