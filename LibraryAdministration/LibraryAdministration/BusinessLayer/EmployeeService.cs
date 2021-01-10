//-----------------------------------------------------------------------
// <copyright file="EmployeeService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.BusinessLayer
{
    using DataAccessLayer;
    using DataMapper;
    using DomainModel;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using Validators;

    /// <summary>
    /// Employee service class
    /// </summary>
    /// <seealso cref="LibraryAdministration.BusinessLayer.BaseService{LibraryAdministration.DomainModel.Employee, LibraryAdministration.Interfaces.DataAccess.IEmployeeRepository}" />
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IEmployeeService" />
    public class EmployeeService : BaseService<Employee, IEmployeeRepository>, IEmployeeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EmployeeService(LibraryContext context)
            : base(new EmployeeRepository(context), new EmployeeValidator())
        {
        }
    }
}
