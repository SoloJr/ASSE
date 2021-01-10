//----------------------------------------------------------------------
// <copyright file="IAuthorService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using DomainModel;

    /// <summary>
    /// IAuthorService interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IService{LibraryAdministration.DomainModel.Author}" />
    public interface IAuthorService : IService<Author>
    {
    }
}
