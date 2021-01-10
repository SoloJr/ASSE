//-----------------------------------------------------------------------
// <copyright file="BookPublisherService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.BusinessLayer
{
    using System.Collections.Generic;
    using DataAccessLayer;
    using DataMapper;
    using DomainModel;
    using Helper;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using Validators;

    /// <summary>
    /// BookPublisher service class
    /// </summary>
    /// <seealso cref="LibraryAdministration.BusinessLayer.BaseService{LibraryAdministration.DomainModel.BookPublisher, LibraryAdministration.Interfaces.DataAccess.IBookPublisherRepository}" />
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IBookPublisherService" />
    public class BookPublisherService : BaseService<BookPublisher, IBookPublisherRepository>, IBookPublisherService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookPublisherService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BookPublisherService(LibraryContext context)
            : base(new BookPublisherRepository(context), new BookPublisherValidator())
        {
        }

        /// <summary>
        /// Gets all editions of book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>All editions of a book</returns>
        /// <exception cref="LibraryArgumentException">bookId is wrong</exception>
        public IEnumerable<BookPublisher> GetAllEditionsOfBook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookId));
            }

            return Repository.GetAllEditionsOfBook(bookId);
        }

        /// <summary>
        /// Checks the book details for availability.
        /// </summary>
        /// <param name="bookPublisherId">The book publisher identifier.</param>
        /// <returns>True if it's available, false otherwise</returns>
        /// <exception cref="LibraryArgumentException">bookPublisherId is wrong</exception>
        public bool CheckBookDetailsForAvailability(int bookPublisherId)
        {
            if (bookPublisherId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookPublisherId));
            }

            return Repository.CheckBookDetailsForAvailability(bookPublisherId);
        }
    }
}
