//-----------------------------------------------------------------------
// <copyright file="PersonalInfoRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataAccessLayer
{
    using DataMapper;
    using DomainModel;
    using Interfaces.DataAccess;

    /// <summary>
    /// PersonalInfo Repository
    /// </summary>
    /// <seealso cref="LibraryAdministration.DataAccessLayer.BaseRepository{LibraryAdministration.DomainModel.PersonalInfo}" />
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IPersonalInfoRepository" />
    public class PersonalInfoRepository : BaseRepository<PersonalInfo>, IPersonalInfoRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalInfoRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PersonalInfoRepository(LibraryContext context)
            : base(context)
        {
        }
    }
}
