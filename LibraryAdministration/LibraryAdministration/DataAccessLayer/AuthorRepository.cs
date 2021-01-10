//-----------------------------------------------------------------------
// <copyright file="AuthorRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataAccessLayer
{
    using DataMapper;
    using DomainModel;
    using Interfaces.DataAccess;

    /// <summary>
    /// AuthorRepository class
    /// </summary>
    /// <seealso cref="LibraryAdministration.DataAccessLayer.BaseRepository{LibraryAdministration.DomainModel.Author}" />
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IAuthorRepository" />
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AuthorRepository(LibraryContext context)
            : base(context)
        {
        }
    }
}
