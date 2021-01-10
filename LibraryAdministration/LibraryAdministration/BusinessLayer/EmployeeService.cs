using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class EmployeeService : BaseService<Employee, IEmployeeRepository>, IEmployeeService
    {
        public EmployeeService(LibraryContext context)
            : base(new EmployeeRepository(context), new EmployeeValidator())
        {

        }
    }
}
