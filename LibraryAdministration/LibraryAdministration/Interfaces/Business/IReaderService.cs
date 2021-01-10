//----------------------------------------------------------------------
// <copyright file="IReaderService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------



namespace LibraryAdministration.Interfaces.Business
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IReaderService interface
    /// </summary>
    /// <seealso cref="Reader" />
    public interface IReaderService : IService<Reader>
    {
        /// <summary>
        /// Checks the employee status.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>the boolean value</returns>
        bool CheckEmployeeStatus(int readerId, int employeeId);

        /// <summary>
        /// Gets all employees that have phone numbers.
        /// </summary>
        /// <returns>Readers list</returns>
        List<Reader> GetAllEmployeesThatHavePhoneNumbers();

        /// <summary>
        /// Gets all employees that have emails.
        /// </summary>
        /// <returns>Readers list</returns>
        List<Reader> GetAllEmployeesThatHaveEmails();

        /// <summary>
        /// Gets the employees that have email and phone numbers set.
        /// </summary>
        /// <returns>Readers list</returns>
        List<Reader> GetEmployeesThatHaveEmailAndPhoneNumbersSet();
    }
}
