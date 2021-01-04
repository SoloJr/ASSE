using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using Ninject.Modules;

namespace LibraryAdministrationTest.Mocks
{
    internal class MockBindings : NinjectModule
    {
        public override void Load()
        {
            LoadRepositoryLayer();
            LoadServiceLayer();
        }

        private void LoadRepositoryLayer()
        {
            Bind<IAuthorService>().To<AuthorServiceMock>();
            Bind<IBookService>().To<BookServiceMock>();
        }

        private void LoadServiceLayer()
        {
            Bind<IAuthorRepository>().To<AuthorRepositoryMock>();
            Bind<IBookRepository>().To<BookRepositoryMock>();
        }
    }
}
