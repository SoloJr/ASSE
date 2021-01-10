//----------------------------------------------------------------------
// <copyright file="IReaderService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using DomainModel;

    /// <summary>
    /// IReaderService interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IService{LibraryAdministration.DomainModel.Reader}" />
    public interface IReaderService : IService<Reader>
    {
        /// <summary>
        /// Checks the employee status.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>the boolean value</returns>
        bool CheckEmployeeStatus(int readerId, int employeeId);
    }
}
