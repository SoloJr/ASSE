//----------------------------------------------------------------------
// <copyright file="IBookPublisherService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IBookPublisherService interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IService{LibraryAdministration.DomainModel.BookPublisher}" />
    public interface IBookPublisherService : IService<BookPublisher>
    {
        /// <summary>
        /// Gets all editions of book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>all editions of book</returns>
        IEnumerable<BookPublisher> GetAllEditionsOfBook(int bookId);

        /// <summary>
        /// Checks the book details for availability.
        /// </summary>
        /// <param name="bookPublisherId">The book publisher identifier.</param>
        /// <returns>boolean value</returns>
        bool CheckBookDetailsForAvailability(int bookPublisherId);
    }
}
