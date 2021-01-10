//-----------------------------------------------------------------------
// <copyright file="ReaderBookService.cs" company="Transilvania University of Brasov">
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
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="LibraryAdministration.BusinessLayer.BaseService{LibraryAdministration.DomainModel.ReaderBook, LibraryAdministration.Interfaces.DataAccess.IReaderBookRepository}" />
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IReaderBookService" />
    public class ReaderBookService : BaseService<ReaderBook, IReaderBookRepository>, IReaderBookService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderBookService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sameAccount">if set to <c>true</c> [same account].</param>
        public ReaderBookService(LibraryContext context, bool sameAccount = true)
            : base(new ReaderBookRepository(context, sameAccount), new ReaderBookValidator())
        {

        }

        /// <summary>
        /// Gets all books on loan.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">readerId</exception>
        public List<ReaderBook> GetAllBooksOnLoan(int readerId)
        {
            if (readerId <= 0)
            {
                throw new LibraryArgumentException(nameof(readerId));
            }

            return _repository.GetAllBooksOnLoan(readerId);
        }

        /// <summary>
        /// Checks the before loan.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">readerId</exception>
        public bool CheckBeforeLoan(int readerId)
        {
            if (readerId <= 0)
            {
                throw new LibraryArgumentException(nameof(readerId));
            }

            return _repository.CheckBeforeLoan(readerId);
        }

        /// <summary>
        /// Checks the past loans for domains.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="domainId">The domain identifier.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">
        /// readerId
        /// or
        /// domainId
        /// </exception>
        public bool CheckPastLoansForDomains(int readerId, int domainId)
        {
            if (readerId <= 0)
            {
                throw new LibraryArgumentException(nameof(readerId));
            }

            if (domainId <= 0)
            {
                throw new LibraryArgumentException(nameof(domainId));
            }

            return _repository.CheckPastLoansForDomains(readerId, domainId);
        }

        /// <summary>
        /// Checks the books rented today.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">readerId</exception>
        public bool CheckBooksRentedToday(int readerId)
        {
            if (readerId <= 0)
            {
                throw new LibraryArgumentException(nameof(readerId));
            }

            return _repository.CheckBooksRentedToday(readerId);
        }

        /// <summary>
        /// Checks the same book rented.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">
        /// readerId
        /// or
        /// bookId
        /// </exception>
        public bool CheckSameBookRented(int bookId, int readerId)
        {
            if (readerId <= 0)
            {
                throw new LibraryArgumentException(nameof(readerId));
            }

            if (bookId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookId));
            }

            return _repository.CheckSameBookRented(bookId, readerId);
        }

        /// <summary>
        /// Extends the loan.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">
        /// id
        /// or
        /// days
        /// </exception>
        /// <exception cref="Exception">Can't extend this loan</exception>
        public ReaderBook ExtendLoan(int id, int days)
        {
            if (id <= 0)
            {
                throw new LibraryArgumentException(nameof(id));
            }

            if (days <= 0)
            {
                throw new LibraryArgumentException(nameof(days));
            }

            if (this.CheckLoanExtension(id, days))
            {
                throw new Exception("Can't extend this loan");
            }

            return _repository.ExtendLoan(id, days);
        }

        /// <summary>
        /// Checks the loan extension.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">
        /// id
        /// or
        /// days
        /// </exception>
        public bool CheckLoanExtension(int id, int days)
        {
            if (id <= 0)
            {
                throw new LibraryArgumentException(nameof(id));
            }

            if (days <= 0)
            {
                throw new LibraryArgumentException(nameof(days));
            }

            return _repository.CheckLoanExtension(id, days);
        }

        /// <summary>
        /// Checks the multiple books domain match.
        /// </summary>
        /// <param name="bookPublisherIds">The book publisher ids.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">bookPublisherIds</exception>
        public bool CheckMultipleBooksDomainMatch(List<int> bookPublisherIds)
        {
            if (bookPublisherIds == null || bookPublisherIds.Count <= 0)
            {
                throw new LibraryArgumentException(nameof(bookPublisherIds));
            }

            return _repository.CheckMultipleBooksDomainMatch(bookPublisherIds);
        }

        /// <summary>
        /// Gets the rent details.
        /// </summary>
        /// <returns></returns>
        public RentDetails GetRentDetails()
        {
            var repo = (ReaderBookRepository)_repository;
            return repo.Details;
        }
    }
}
