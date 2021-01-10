using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IReaderRepository : IRepository<Reader>
    {
        bool CheckEmployeeStatus(int readerId, int employeeId);
    }
}
