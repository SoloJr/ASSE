//---------------------------------------------------------
// <copyright file="IReaderRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.DataAccess
{
    using DomainModel;

    /// <summary>
    /// IReaderRepository interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{LibraryAdministration.DomainModel.Reader}" />
    public interface IReaderRepository : IRepository<Reader>
    {
        /// <summary>
        /// Checks the employee status.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>boolean value</returns>
        bool CheckEmployeeStatus(int readerId, int employeeId);
    }
}
