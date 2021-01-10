using LibraryAdministration.DomainModel;
using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IReaderBookRepository : IRepository<ReaderBook>
    {
        List<ReaderBook> GetAllBooksOnLoan(int readerId);

        bool CheckBeforeLoan(int readerId);

        bool CheckPastLoansForDomains(int readerId, int domainId);

        bool CheckBooksRentedToday(int readerId);

        bool CheckSameBookRented(int bookId, int readerId);

        bool CheckLoanExtension(int id, int days);

        ReaderBook ExtendLoan(int id, int days);

        bool CheckMultipleBooksDomainMatch(List<int> bookPublisherIds);
    }
}
