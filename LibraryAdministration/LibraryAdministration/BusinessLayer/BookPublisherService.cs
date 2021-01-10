//-----------------------------------------------------------------------
// <copyright file="BookPublisherService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.BusinessLayer
{
    using DataAccessLayer;
    using DataMapper;
    using DomainModel;
    using Helper;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using Validators;
    using System.Collections.Generic;

    /// <summary>
    /// 
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
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">bookId</exception>
        public IEnumerable<BookPublisher> GetAllEditionsOfBook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookId));
            }

            return _repository.GetAllEditionsOfBook(bookId);
        }

        /// <summary>
        /// Checks the book details for availability.
        /// </summary>
        /// <param name="bookPublisherId">The book publisher identifier.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">bookPublisherId</exception>
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
