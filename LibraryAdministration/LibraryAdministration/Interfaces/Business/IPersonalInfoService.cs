//----------------------------------------------------------------------
// <copyright file="IPersonalInfoService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using DomainModel;

    /// <summary>
    /// IPersonalInfoService interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IService{LibraryAdministration.DomainModel.PersonalInfo}" />
    public interface IPersonalInfoService : IService<PersonalInfo>
    {
    }
}
