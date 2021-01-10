//---------------------------------------------------------
// <copyright file="IBookPublisherRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.DataAccess
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IBookPublisherRepository interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{LibraryAdministration.DomainModel.BookPublisher}" />
    public interface IBookPublisherRepository : IRepository<BookPublisher>
    {
        /// <summary>
        /// Gets all editions of book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>all editions of a book</returns>
        IEnumerable<BookPublisher> GetAllEditionsOfBook(int bookId);

        /// <summary>
        /// Checks the book details for availability.
        /// </summary>
        /// <param name="bookPublisherId">The book publisher identifier.</param>
        /// <returns>boolean value</returns>
        bool CheckBookDetailsForAvailability(int bookPublisherId);
    }
}
