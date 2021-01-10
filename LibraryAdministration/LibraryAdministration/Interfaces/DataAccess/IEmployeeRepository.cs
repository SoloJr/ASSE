//---------------------------------------------------------
// <copyright file="IEmployeeRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.DataAccess
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IEmployeeRepository interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{LibraryAdministration.DomainModel.Employee}" />
    public interface IEmployeeRepository : IRepository<Employee>
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
