//----------------------------------------------------------------------
// <copyright file="IReaderBookService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using System.Collections.Generic;
    using DomainModel;
    using Helper;

    /// <summary>
    /// IReaderBookService interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IService{LibraryAdministration.DomainModel.ReaderBook}" />
    public interface IReaderBookService : IService<ReaderBook>
    {
        /// <summary>
        /// Gets all books on loan.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns>All books on loan</returns>
        List<ReaderBook> GetAllBooksOnLoan(int readerId);

        /// <summary>
        /// Checks the before loan.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns>the boolean value</returns>
        bool CheckBeforeLoan(int readerId);

        /// <summary>
        /// Checks the past loans for domains.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="domainId">The domain identifier.</param>
        /// <returns>the boolean value</returns>
        bool CheckPastLoansForDomains(int readerId, int domainId);

        /// <summary>
        /// Checks the books rented today.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns>the boolean value</returns>
        bool CheckBooksRentedToday(int readerId);

        /// <summary>
        /// Checks the same book rented.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns>the boolean value</returns>
        bool CheckSameBookRented(int bookId, int readerId);

        /// <summary>
        /// Checks the loan extension.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>the boolean value</returns>
        bool CheckLoanExtension(int id, int days);

        /// <summary>
        /// Extends the loan.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>Reader Book object</returns>
        ReaderBook ExtendLoan(int id, int days);

        /// <summary>
        /// Gets the rent details.
        /// </summary>
        /// <returns>Rent Details object</returns>
        RentDetails GetRentDetails();

        /// <summary>
        /// Checks the multiple books domain match.
        /// </summary>
        /// <param name="bookPublisherIds">The book publisher ids.</param>
        /// <returns>the boolean value</returns>
        bool CheckMultipleBooksDomainMatch(List<int> bookPublisherIds);
    }
}
