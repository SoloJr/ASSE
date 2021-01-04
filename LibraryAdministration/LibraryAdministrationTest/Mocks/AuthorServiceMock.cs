using System;
using System.Collections.Generic;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministrationTest.Mocks
{
    public class AuthorServiceMock : BaseService<Author, IAuthorRepository>, IAuthorService
    {
        public AuthorServiceMock()
            : base(Injector.Get<IAuthorRepository>(), new AuthorValidator())
        {

        }

        public IEnumerable<Author> GetAuthorsWithBooks()
        {
            throw new NotImplementedException();
        }
    }
}
