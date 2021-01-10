//-----------------------------------------------------------------------
// <copyright file="EmployeeRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataAccessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using Interfaces.DataAccess;

    /// <summary>
    /// Employee Repository class
    /// </summary>
    /// <seealso cref="Employee" />
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IEmployeeRepository" />
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EmployeeRepository(LibraryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all employees that have phone numbers.
        /// </summary>
        /// <returns>Employee list</returns>
        public List<Employee> GetAllEmployeesThatHavePhoneNumbers()
        {
            return Context.Employees.Where(x => x.Info.PhoneNumber != string.Empty).ToList();
        }

        /// <summary>
        /// Gets all employees that have emails.
        /// </summary>
        /// <returns>Employee list</returns>
        public List<Employee> GetAllEmployeesThatHaveEmails()
        {
            return Context.Employees.Where(x => x.Info.Email != string.Empty).ToList();
        }

        /// <summary>
        /// Gets the employees that have email and phone numbers set.
        /// </summary>
        /// <returns>Employee list</returns>
        public List<Employee> GetEmployeesThatHaveEmailAndPhoneNumbersSet()
        {
            return Context.Employees.Where(x => x.Info.PhoneNumber != string.Empty && x.Info.Email != string.Empty).ToList();
        }
    }
}
