using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class ReaderService : BaseService<Reader, IReaderRepository>, IReaderService
    {
        public ReaderService(LibraryContext context)
            : base(new ReaderRepository(context), new ReaderValidator())
        {

        }

        public bool CheckEmployeeStatus(int readerId, int employeeId)
        {
            if (readerId <= 0)
            {
                throw new LibraryArgumentException(nameof(readerId));
            }

            if (employeeId <= 0)
            {
                throw new LibraryArgumentException(nameof(employeeId));
            }

            return _repository.CheckEmployeeStatus(readerId, employeeId);
        }
    }
}
