using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.DataAccess;
using System.Linq;

namespace LibraryAdministration.DataAccessLayer
{
    public class ReaderRepository : BaseRepository<Reader>, IReaderRepository
    {
        public ReaderRepository(LibraryContext context)
            : base(context)
        {

        }

        public bool CheckEmployeeStatus(int readerId, int employeeId)
        {
            var reader = _context.Readers.FirstOrDefault(x => x.Id == readerId);
            var employee = _context.Employees.FirstOrDefault(x => x.Id == employeeId);

            if (reader == null || employee == null)
            {
                return false;
            }

            return reader.ReaderPersonalInfoId == employee.EmployeePersonalInfoId;
        }
    }
}
