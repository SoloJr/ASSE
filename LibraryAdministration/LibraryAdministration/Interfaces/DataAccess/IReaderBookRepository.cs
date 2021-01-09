using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IReaderBookRepository : IRepository<ReaderBook>
    {
        List<ReaderBook> GetAllBooksOnLoan(int readerId);

        bool CheckBeforeLoan(int readerId);

        bool CheckPastLoansForDomains(int readerId, int domainId);

        bool CheckBooksRentedToday(int readerId);

        bool CheckSameBookRented(int bookId, int readerId);

    }
}
