//----------------------------------------------------------------------
// <copyright file="IPublisherService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IPublisherService interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IService{LibraryAdministration.DomainModel.Publisher}" />
    public interface IPublisherService : IService<Publisher>
    {
        /// <summary>
        /// Gets all book publishers of a book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>All book publishers of a book</returns>
        ICollection<Publisher> GetAllBookPublishersOfABook(int bookId);
    }
}
