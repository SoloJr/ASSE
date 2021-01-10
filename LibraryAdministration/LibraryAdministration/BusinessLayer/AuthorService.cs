//-----------------------------------------------------------------------
// <copyright file="AuthorService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.BusinessLayer
{
    using DataAccessLayer;
    using DataMapper;
    using DomainModel;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using Validators;

    /// <summary>
    /// Author Service class
    /// </summary>
    /// <seealso cref="LibraryAdministration.BusinessLayer.BaseService{LibraryAdministration.DomainModel.Author, LibraryAdministration.Interfaces.DataAccess.IAuthorRepository}" />
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IAuthorService" />
    public class AuthorService : BaseService<Author, IAuthorRepository>, IAuthorService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AuthorService(LibraryContext context)
            : base(new AuthorRepository(context), new AuthorValidator())
        {
        }
    }
}
