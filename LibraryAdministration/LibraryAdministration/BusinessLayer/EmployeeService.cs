//-----------------------------------------------------------------------
// <copyright file="EmployeeService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

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

        /// <summary>
        /// Gets all employees that have phone numbers.
        /// </summary>
        /// <returns>Employee list</returns>
        public List<Employee> GetAllEmployeesThatHavePhoneNumbers()
        {
            return Repository.GetAllEmployeesThatHavePhoneNumbers();
        }

        /// <summary>
        /// Gets all employees that have emails.
        /// </summary>
        /// <returns>Employee list</returns>
        public List<Employee> GetAllEmployeesThatHaveEmails()
        {
            return Repository.GetAllEmployeesThatHaveEmails();
        }

        /// <summary>
        /// Gets the employees that have email and phone numbers set.
        /// </summary>
        /// <returns>Employee list</returns>
        public List<Employee> GetEmployeesThatHaveEmailAndPhoneNumbersSet()
        {
            return Repository.GetEmployeesThatHaveEmailAndPhoneNumbersSet();
        }
    }
}
