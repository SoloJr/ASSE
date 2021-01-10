//-----------------------------------------------------------------------
// <copyright file="PersonalInfoService.cs" company="Transilvania University of Brasov">
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
    /// 
    /// </summary>
    /// <seealso cref="LibraryAdministration.BusinessLayer.BaseService{LibraryAdministration.DomainModel.PersonalInfo, LibraryAdministration.Interfaces.DataAccess.IPersonalInfoRepository}" />
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IPersonalInfoService" />
    public class PersonalInfoService : BaseService<PersonalInfo, IPersonalInfoRepository>, IPersonalInfoService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalInfoService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PersonalInfoService(LibraryContext context)
            : base(new PersonalInfoRepository(context), new PersonalInfoValidator())
        {

        }
    }
}
