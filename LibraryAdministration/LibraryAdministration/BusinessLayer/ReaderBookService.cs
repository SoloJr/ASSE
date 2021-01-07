using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class ReaderBookService : BaseService<ReaderBook, IReaderBookRepository>, IReaderBookService
    {
        public ReaderBookService(LibraryContext context)
            : base(new ReaderBookRepository(context), new ReaderBookValidator())
        {

        }

        public List<ReaderBook> GetAllBooksOnLoan(int readerId)
        {
            var repo = (ReaderBookRepository) _repository;
            return repo.GetAllBooksOnLoan(readerId);
        }

        public bool CheckBeforeLoan(int readerId)
        {
            return _repository.CheckBeforeLoan(readerId);
        }

        public bool CheckPastLoansForDomains(int readerId, int domainId)
        {
            return _repository.CheckPastLoansForDomains(readerId, domainId);
        }

        public bool CheckBooksRentedToday(int readerId)
        {
            return _repository.CheckBooksRentedToday(readerId);
        }
    }
}
