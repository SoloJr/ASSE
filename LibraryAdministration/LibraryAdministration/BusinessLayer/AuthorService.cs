using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class AuthorService : BaseService<Author, IAuthorRepository>, IAuthorService
    {
        public AuthorService()
            : base(Injector.Get<IAuthorRepository>(), new AuthorValidator())
        {
            
        }


        public IEnumerable<Author> GetAuthorsWithBooks()
        {
            return _repository.Get(
                filter: author => author.Books.Count > 0,
                includeProperties: "Books");
        }
    }
}
