using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IReaderBookService : IService<ReaderBook>
    {
        List<ReaderBook> GetAllBooksOnLoan(int readerId);

        bool CheckBeforeLoan(int readerId);

        bool CheckPastLoansForDomains(int readerId, int domainId);

        bool CheckBooksRentedToday(int readerId);

        bool CheckSameBookRented(int bookId, int readerId);

        bool CheckLoanExtension(int id, int days);

        ReaderBook ExtendLoan(int id, int days);

        RentDetails GetRentDetails();

        bool CheckMultipleBooksDomainMatch(List<int> bookPublisherIds);
    }
}
