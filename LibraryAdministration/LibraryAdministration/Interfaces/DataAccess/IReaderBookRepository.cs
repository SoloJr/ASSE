//---------------------------------------------------------
// <copyright file="IReaderBookRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.DataAccess
{
    using System;
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IReaderBookRepository interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{LibraryAdministration.DomainModel.ReaderBook}" />
    public interface IReaderBookRepository : IRepository<ReaderBook>
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
        /// <returns>boolean value</returns>
        bool CheckBeforeLoan(int readerId);

        /// <summary>
        /// Checks the past loans for domains.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="domainId">The domain identifier.</param>
        /// <returns>boolean value</returns>
        bool CheckPastLoansForDomains(int readerId, int domainId);

        /// <summary>
        /// Checks the books rented today.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="isEmployee">if set to <c>true</c> [is employee].</param>
        /// <returns>boolean value</returns>
        bool CheckBooksRentedToday(int readerId, bool isEmployee = false);

        /// <summary>
        /// Checks the same book rented.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns>boolean value</returns>
        bool CheckSameBookRented(int bookId, int readerId);

        /// <summary>
        /// Checks the loan extension.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>boolean value</returns>
        bool CheckLoanExtension(int id, int days);

        /// <summary>
        /// Extends the loan.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>ReaderBook object</returns>
        ReaderBook ExtendLoan(int id, int days);

        /// <summary>
        /// Checks the multiple books domain match.
        /// </summary>
        /// <param name="bookPublisherIds">The book publisher ids.</param>
        /// <returns>boolean value</returns>
        bool CheckMultipleBooksDomainMatch(List<int> bookPublisherIds);

        /// <summary>
        /// Gets all books rented in between dates.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>books rented</returns>
        List<BookPublisher> GetAllBooksRentedInBetweenDates(int readerId, DateTime start, DateTime end);
    }
}
