using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IReaderService : IService<Reader>
    {
        bool CheckEmployeeStatus(int readerId, int employeeId);
    }
}
