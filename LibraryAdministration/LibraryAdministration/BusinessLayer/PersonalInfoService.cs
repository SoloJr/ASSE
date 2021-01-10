using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class PersonalInfoService : BaseService<PersonalInfo, IPersonalInfoRepository>, IPersonalInfoService
    {
        public PersonalInfoService(LibraryContext context)
            : base(new PersonalInfoRepository(context), new PersonalInfoValidator())
        {

        }
    }
}
