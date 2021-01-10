using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.DataAccess;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
namespace LibraryAdministration.DataAccessLayer
{
    public class BookPublisherRepository : BaseRepository<BookPublisher>, IBookPublisherRepository
    {
        public BookPublisherRepository(LibraryContext context) : base(context) { }


        public IEnumerable<BookPublisher> GetAllEditionsOfBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == bookId) ?? throw new ObjectNotFoundException("Book not found");

            return _context.BookPublisher.Where(x => x.BookId == book.Id).ToList();
        }

        public bool CheckBookDetailsForAvailability(int bookPublisherId)
        {
            var bp = _context.BookPublisher.FirstOrDefault(x => x.Id == bookPublisherId)
                ?? throw new ObjectNotFoundException("Book not found");

            if (bp.ForRent <= 0)
            {
                return false;
            }

            return bp.RentCount < (bp.ForRent - (bp.ForRent / 10));
        }
    }
}
