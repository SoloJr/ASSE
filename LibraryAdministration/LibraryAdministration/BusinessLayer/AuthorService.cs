using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class AuthorService : BaseService<Author, IAuthorRepository>, IAuthorService
    {
        public AuthorService(LibraryContext context)
            : base(new AuthorRepository(context), new AuthorValidator())
        {

        }
    }
}
