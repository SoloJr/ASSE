//-----------------------------------------------------------------------
// <copyright file="ReaderBookRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Core;
    using System.Linq;
    using BusinessLayer;
    using DataMapper;
    using DomainModel;
    using Helper;
    using Interfaces.DataAccess;

    /// <summary>
    /// ReaderBook Repository class
    /// </summary>
    /// <seealso cref="LibraryAdministration.DataAccessLayer.BaseRepository{LibraryAdministration.DomainModel.ReaderBook}" />
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IReaderBookRepository" />
    public class ReaderBookRepository : BaseRepository<ReaderBook>, IReaderBookRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderBookRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="isSameAccount">if set to <c>true</c> [is same account].</param>
        public ReaderBookRepository(LibraryContext context, bool isSameAccount = true)
            : base(context)
        {
            this.InitRentDetails(isSameAccount);
        }

        /// <summary>
        /// Gets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public RentDetails Details { get; private set; }

        /// <summary>
        /// Gets all books on loan.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns>List of books on loan</returns>
        public List<ReaderBook> GetAllBooksOnLoan(int readerId)
        {
            var date = DateTime.Now.AddDays(-this.Details.PER);
            return Context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();
        }

        /// <summary>
        /// Checks the before loan.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns>boolean value</returns>
        public bool CheckBeforeLoan(int readerId)
        {
            var date = DateTime.Now.AddDays(-this.Details.PER);
            var result = Context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();

            return result.Count <= this.Details.NMC;
        }

        /// <summary>
        /// Checks the past loans for domains.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="domainId">The domain identifier.</param>
        /// <returns>boolean value</returns>
        public bool CheckPastLoansForDomains(int readerId, int domainId)
        {
            var date = DateTime.Now.AddMonths(-this.Details.L);
            var domainService = new DomainService(Context);
            var domains = domainService.GetAllParentDomains(domainId);
            var allLoans = Context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();
            var count = 0;
            var domainIds = domains.Select(x => x.Id).ToList();
            foreach (var loan in allLoans)
            {
                var domainOfLoan = loan.BookPublisher.Book.Domains.ToList();
                foreach (var domain in domainOfLoan)
                {
                    var parentDomains = domainService.GetAllParentDomains(domain.Id).Select(x => x.Id).ToList();
                    if (domainIds.Contains(domain.Id))
                    {
                        count++;
                    }

                    count += parentDomains.Count(it => domainIds.Contains(it));
                }
            }

            return count <= this.Details.D;
        }

        /// <summary>
        /// Checks the books rented today.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns>boolean value</returns>
        public bool CheckBooksRentedToday(int readerId)
        {
            var date = DateTime.Today;
            var result = Context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();

            return result.Count <= this.Details.NCZ;
        }

        /// <summary>
        /// Checks the same book rented.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="readerId">The reader identifier.</param>
        /// <returns>boolean value</returns>
        public bool CheckSameBookRented(int bookId, int readerId)
        {
            var data = Context.ReaderBooks.Where(x => x.ReaderId == readerId && x.BookPublisher.BookId == bookId).ToList();

            var ddlDate = DateTime.Now.AddDays(-this.Details.DELTA);

            return data.Count == 0 || data.All(rb => rb.LoanDate <= ddlDate);
        }

        /// <summary>
        /// Checks the loan extension.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>boolean value</returns>
        /// <exception cref="ObjectNotFoundException">Loan object not found</exception>
        /// <exception cref="LoanExtensionException">Error in extending loan availability</exception>
        public bool CheckLoanExtension(int id, int days)
        {
            var loan = Context.ReaderBooks.FirstOrDefault(x => x.Id == id) ??
                       throw new ObjectNotFoundException("Loan not found");

            if (loan.ExtensionDays + days > this.Details.LIM)
            {
                throw new LoanExtensionException();
            }

            return true;
        }

        /// <summary>
        /// Extends the loan.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="days">The days.</param>
        /// <returns>The ReaderBook updated object</returns>
        /// <exception cref="ObjectNotFoundException">Loan not found</exception>
        public ReaderBook ExtendLoan(int id, int days)
        {
            var loan = Context.ReaderBooks.First(x => x.Id == id)
                ?? throw new ObjectNotFoundException("Loan not found");

            loan.ExtensionDays = days;

            loan.DueDate = loan.DueDate.AddDays(days);

            this.Update(loan);

            return loan;
        }

        /// <summary>
        /// Checks the multiple books domain match.
        /// </summary>
        /// <param name="bookPublisherIds">The book publisher ids.</param>
        /// <returns>boolean value</returns>
        public bool CheckMultipleBooksDomainMatch(List<int> bookPublisherIds)
        {
            if (bookPublisherIds.Count <= this.Details.C)
            {
                return true;
            }

            var list = bookPublisherIds
                .Select(id => Context.BookPublisher.FirstOrDefault(x => x.Id == id) ?? throw new ObjectNotFoundException()).ToList();

            var books = list.Select(id => Context.Books.FirstOrDefault(x => x.Id == id.BookId)).Distinct().ToList();

            var domainIds = new HashSet<int>();

            foreach (var domain in books.SelectMany(book => book.Domains))
            {
                domainIds.Add(domain.Id);
            }

            return domainIds.Count >= 2;
        }

        /// <summary>
        /// Gets all books rented in between dates.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>books rented</returns>
        public List<BookPublisher> GetAllBooksRentedInBetweenDates(int readerId, DateTime start, DateTime end)
        {
            return Context.ReaderBooks.Where(x => 
                x.ReaderId == readerId &&
                x.LoanDate >= start &&
                x.DueDate < end
            ).Select(x => x.BookPublisher).ToList();
        }

        /// <summary>
        /// Initializes the rent details.
        /// </summary>
        /// <param name="isSameAccount">if set to <c>true</c> [is same account].</param>
        private void InitRentDetails(bool isSameAccount)
        {
            var per = ConfigurationManager.AppSettings["PER"];
            var nmc = ConfigurationManager.AppSettings["NMC"];
            var d = ConfigurationManager.AppSettings["D"];
            var l = ConfigurationManager.AppSettings["L"];
            var ncz = ConfigurationManager.AppSettings["NCZ"];
            var delta = ConfigurationManager.AppSettings["DELTA"];
            var c = ConfigurationManager.AppSettings["C"];
            var lim = ConfigurationManager.AppSettings["LIM"];
            var persimp = ConfigurationManager.AppSettings["PERSIMP"];
            this.Details = new RentDetails
            {
                C = int.Parse(c),
                D = int.Parse(d),
                DELTA = int.Parse(delta),
                LIM = int.Parse(lim),
                NCZ = int.Parse(ncz),
                NMC = int.Parse(nmc),
                PER = int.Parse(per),
                PERSIMP = int.Parse(persimp),
                L = int.Parse(l)
            };

            if (isSameAccount != true)
            {
                return;
            }

            this.Details.NMC *= 2;
            this.Details.C *= 2;
            this.Details.D *= 2;
            this.Details.LIM *= 2;
            this.Details.DELTA = (int)(this.Details.DELTA / 2);
            this.Details.PER = (int)(this.Details.PER / 2);
        }
    }
}
