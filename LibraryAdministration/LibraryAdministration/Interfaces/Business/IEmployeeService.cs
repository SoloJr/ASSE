//----------------------------------------------------------------------
// <copyright file="IEmployeeService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using DomainModel;

    /// <summary>
    /// IEmployeeService interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IService{LibraryAdministration.DomainModel.Employee}" />
    public interface IEmployeeService : IService<Employee>
    {
    }
}
