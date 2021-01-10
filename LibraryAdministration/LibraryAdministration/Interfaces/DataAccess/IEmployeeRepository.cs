//---------------------------------------------------------
// <copyright file="IEmployeeRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.DataAccess
{
    using DomainModel;

    /// <summary>
    /// IEmployeeRepository interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{LibraryAdministration.DomainModel.Employee}" />
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}
