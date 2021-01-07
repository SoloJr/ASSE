using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;

namespace LibraryAdministration.DataAccessLayer
{
    public class ReaderBookRepository : BaseRepository<ReaderBook>, IReaderBookRepository
    {
        public ReaderBookRepository(LibraryContext context)
            : base(context)
        {
            
        }

        public List<ReaderBook> GetAllBooksOnLoan(int readerId)
        {
            var per = ConfigurationManager.AppSettings["PER"];
            var perInt = int.Parse(per);
            using (_context)
            {
                var date = DateTime.Now.AddDays(-perInt);
                return _context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();
            }
        }

        public bool CheckBeforeLoan(int readerId)
        {
            var per = ConfigurationManager.AppSettings["PER"];
            var nmc = ConfigurationManager.AppSettings["NMC"];
            var perInt = int.Parse(per);
            var nmcInt = int.Parse(nmc);
            using (_context)
            {
                var date = DateTime.Now.AddDays(-perInt);
                var result = _context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();
                if (result.Count > nmcInt)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckPastLoansForDomains(int readerId, int domainId)
        {
            var d = ConfigurationManager.AppSettings["D"];
            var l = ConfigurationManager.AppSettings["L"];
            var dInt = int.Parse(d);
            var lInt = int.Parse(l);
            var date = DateTime.Now.AddMonths(-lInt);
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

            return count <= dInt;
        }

        public bool CheckBooksRentedToday(int readerId)
        {
            var ncz = ConfigurationManager.AppSettings["NCZ"];
            var nczInt = int.Parse(ncz);
            var date = DateTime.Today;
            var result = _context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date && x.LoanReturnDate == null).ToList();

            return result.Count <= nczInt;
        }

        public bool CheckSameBookRented(int bookId, int readerId)
        {
            var delta = ConfigurationManager.AppSettings["DELTA"];
            var deltaInt = int.Parse(delta);
            var data = _context.ReaderBooks.Where(x => x.ReaderId == readerId && x.BookPublisher.BookId == bookId).ToList();

            var ddlDate = DateTime.Now.AddDays(-deltaInt);

            return data.Count == 0 || data.All(rb => rb.LoanDate <= ddlDate);
        }
    }
}
