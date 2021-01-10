//---------------------------------------------------------
// <copyright file="IBookRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.DataAccess
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IBookRepository interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{LibraryAdministration.DomainModel.Book}" />
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Gets all domains of book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>all domains of book</returns>
        IEnumerable<Domain> GetAllDomainsOfBook(int bookId);
    }
}
