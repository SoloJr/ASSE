//---------------------------------------------------------
// <copyright file="IPublisherRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.DataAccess
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IPublisherRepository interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{LibraryAdministration.DomainModel.Publisher}" />
    public interface IPublisherRepository : IRepository<Publisher>
    {
        /// <summary>
        /// Gets all book publishers of a book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>all book publishers of a book></returns>
        ICollection<Publisher> GetAllBookPublishersOfABook(int bookId);
    }
}
