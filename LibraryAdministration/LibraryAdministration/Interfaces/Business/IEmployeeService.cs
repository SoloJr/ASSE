//----------------------------------------------------------------------
// <copyright file="IEmployeeService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IEmployeeService interface
    /// </summary>
    /// <seealso cref="Employee" />
    public interface IEmployeeService : IService<Employee>
    {
        /// <summary>
        /// Gets all employees that have phone numbers.
        /// </summary>
        /// <returns>Employee list</returns>
        List<Employee> GetAllEmployeesThatHavePhoneNumbers();

        /// <summary>
        /// Gets all employees that have emails.
        /// </summary>
        /// <returns>Employee list</returns>
        List<Employee> GetAllEmployeesThatHaveEmails();

        /// <summary>
        /// Gets the employees that have email and phone numbers set.
        /// </summary>
        /// <returns>Employee list</returns>
        List<Employee> GetEmployeesThatHaveEmailAndPhoneNumbersSet();
    }
}
