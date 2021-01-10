//-----------------------------------------------------------------------
// <copyright file="BookPublisherRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataAccessLayer
{
    using System.Collections.Generic;
    using System.Data.Entity.Core;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using Interfaces.DataAccess;

    /// <summary>
    /// BookPublisher Repository class
    /// </summary>
    /// <seealso cref="LibraryAdministration.DataAccessLayer.BaseRepository{LibraryAdministration.DomainModel.BookPublisher}" />
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IBookPublisherRepository" />
    public class BookPublisherRepository : BaseRepository<BookPublisher>, IBookPublisherRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookPublisherRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BookPublisherRepository(LibraryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all editions of book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>All editions of a book</returns>
        /// <exception cref="ObjectNotFoundException">Book not found</exception>
        public IEnumerable<BookPublisher> GetAllEditionsOfBook(int bookId)
        {
            var book = Context.Books.FirstOrDefault(x => x.Id == bookId) ?? throw new ObjectNotFoundException("Book not found");

            return Context.BookPublisher.Where(x => x.BookId == book.Id).ToList();
        }

        /// <summary>
        /// Checks the book details for availability.
        /// </summary>
        /// <param name="bookPublisherId">The book publisher identifier.</param>
        /// <returns>boolean value</returns>
        /// <exception cref="ObjectNotFoundException">Book not found</exception>
        public bool CheckBookDetailsForAvailability(int bookPublisherId)
        {
            var bp = Context.BookPublisher.FirstOrDefault(x => x.Id == bookPublisherId)
                ?? throw new ObjectNotFoundException("Book not found");

            if (bp.ForRent <= 0)
            {
                return false;
            }

            return bp.RentCount < (bp.ForRent - (bp.ForRent / 10));
        }

        /// <summary>
        /// Gets the type of the books by.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>List of books</returns>
        public List<BookPublisher> GetBooksByType(BookType type)
        {
            return Context.BookPublisher.Where(x => x.Type == type).ToList();
        }
    }
}
