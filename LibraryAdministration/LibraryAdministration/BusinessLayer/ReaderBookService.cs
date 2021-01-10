using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class ReaderBookService : BaseService<ReaderBook, IReaderBookRepository>, IReaderBookService
    {
        public ReaderBookService(LibraryContext context, bool sameAccount = true)
            : base(new ReaderBookRepository(context, sameAccount), new ReaderBookValidator())
        {
            
        }

        public List<ReaderBook> GetAllBooksOnLoan(int readerId)
        {
            if (readerId <= 0)
            {
                throw new LibraryArgumentException(nameof(readerId));
            }

            return _repository.GetAllBooksOnLoan(readerId);
        }

        public bool CheckBeforeLoan(int readerId)
        {
            if (readerId <= 0)
            {
                throw new LibraryArgumentException(nameof(readerId));
            }

            return _repository.CheckBeforeLoan(readerId);
        }

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

        public bool CheckBooksRentedToday(int readerId)
        {
            if (readerId <= 0)
            {
                throw new LibraryArgumentException(nameof(readerId));
            }

            return _repository.CheckBooksRentedToday(readerId);
        }

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

        public RentDetails GetRentDetails()
        {
            var repo = (ReaderBookRepository)_repository;
            return repo.Details;
        }
    }
}
