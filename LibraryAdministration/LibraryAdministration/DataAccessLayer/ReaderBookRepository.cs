using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;

namespace LibraryAdministration.DataAccessLayer
{
    public class ReaderBookRepository : BaseRepository<ReaderBook>, IReaderBookRepository
    {
        public RentDetails Details { get; private set; }

        public ReaderBookRepository(LibraryContext context, bool isSameAccount = true)
            : base(context)
        {
            InitRentDetails(isSameAccount);
        }

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
            Details = new RentDetails
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

            if (isSameAccount != true) return;

            Details.NMC *= 2;
            Details.C *= 2;
            Details.D *= 2;
            Details.LIM *= 2;
            Details.DELTA = (int) (Details.DELTA / 2);
            Details.PER = (int)(Details.PER / 2);
        }

        public List<ReaderBook> GetAllBooksOnLoan(int readerId)
        {
            using (_context)
            {
                var date = DateTime.Now.AddDays(-(Details.PER));
                return _context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();
            }
        }

        public bool CheckBeforeLoan(int readerId)
        {
            using (_context)
            {
                var date = DateTime.Now.AddDays(-(Details.PER));
                var result = _context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();
                if (result.Count > Details.NMC)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckPastLoansForDomains(int readerId, int domainId)
        {
            var date = DateTime.Now.AddMonths(-(Details.L));
            var domainService = new DomainService(_context);
            var domains = domainService.GetAllParentDomains(domainId);
            var allLoans = _context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();
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

                    foreach (var it in parentDomains)
                    {
                        if (domainIds.Contains(it))
                        {
                            count++;
                        }
                    }
                }
            }

            return count <= Details.D;
        }

        public bool CheckBooksRentedToday(int readerId)
        {
            var date = DateTime.Today;
            var result = _context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();

            return result.Count <= Details.NCZ;
        }

        public bool CheckSameBookRented(int bookId, int readerId)
        {
            var data = _context.ReaderBooks.Where(x => x.ReaderId == readerId && x.BookPublisher.BookId == bookId).ToList();

            var ddlDate = DateTime.Now.AddDays(-(Details.DELTA));

            return data.Count == 0 || data.All(rb => rb.LoanDate <= ddlDate);
        }

        public bool CheckLoanExtension(int id, int days)
        {
            var loan = _context.ReaderBooks.FirstOrDefault(x => x.Id == id) ??
                       throw new ObjectNotFoundException("Loan not found");

            if (loan.ExtensionDays + days > Details.LIM)
            {
                throw new LoanExtensionException();
            }

            return true;
        }

        public ReaderBook ExtendLoan(int id, int days)
        {
            var loan = _context.ReaderBooks.First(x => x.Id == id)
                ?? throw new ObjectNotFoundException("Loan not found");

            loan.ExtensionDays = days;

            loan.DueDate = loan.DueDate.AddDays(days);

            this.Update(loan);

            return loan;
        }

        public bool CheckMultipleBooksDomainMatch(List<int> bookPublisherIds)
        {
            if (bookPublisherIds.Count <= Details.C)
            {
                return true;
            }

            var list = bookPublisherIds
                .Select(id => _context.BookPublisher.FirstOrDefault(x => x.Id == id) ?? throw new ObjectNotFoundException()).ToList();

            var books = list.Select(id => _context.Books.FirstOrDefault(x => x.Id == id.BookId)).Distinct().ToList();

            var domainIds = new HashSet<int>();

            foreach (var domain in books.SelectMany(book => book.Domains))
            {
                domainIds.Add(domain.Id);
            }

            return domainIds.Count >= 2;
        }
    }
}
