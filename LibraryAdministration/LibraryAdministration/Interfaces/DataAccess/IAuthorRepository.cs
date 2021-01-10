//----------------------------------------------------------------------
// <copyright file="IAuthorRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.DataAccess
{
    using DomainModel;

    /// <summary>
    /// IAuthorRepository interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{LibraryAdministration.DomainModel.Author}" />
    public interface IAuthorRepository : IRepository<Author>
    {
    }
}
