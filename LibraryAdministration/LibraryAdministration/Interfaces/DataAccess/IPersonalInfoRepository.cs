//---------------------------------------------------------
// <copyright file="IPersonalInfoRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.DataAccess
{
    using DomainModel;

    /// <summary>
    /// IPersonalInfoRepository interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{LibraryAdministration.DomainModel.PersonalInfo}" />
    public interface IPersonalInfoRepository : IRepository<PersonalInfo>
    {
    }
}
