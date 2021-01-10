//----------------------------------------------------------------------
// <copyright file="IBookService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IBookService interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IService{LibraryAdministration.DomainModel.Book}" />
    public interface IBookService : IService<Book>
    {
        /// <summary>
        /// Gets all domains of book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>all domains of book</returns>
        IEnumerable<Domain> GetAllDomainsOfBook(int bookId);
    }
}
