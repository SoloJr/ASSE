using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.BusinessLayer;
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
            Bind<IAuthorService>().To<AuthorService>();
            Bind<IBookService>().To<BookService>();
            Bind<IBookPublisherService>().To<BookPublisherService>();
            Bind<IBookRentalService>().To<BookRentalService>();
            Bind<IDomainService>().To<DomainService>();
            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<IPersonalInfoService>().To<PersonalInfoService>();
            Bind<IPublisherService>().To<PublisherService>();
            Bind<IReaderService>().To<ReaderService>();
            Bind<IReaderBookService>().To<ReaderBookService>();
        }

        private void LoadServiceLayer()
        {
            Bind<IAuthorRepository>().To<AuthorRepositoryMock>();
            Bind<IBookRepository>().To<BookRepositoryMock>();
            Bind<IBookPublisherRepository>().To<BookPublisherRepositoryMock>();
            Bind<IBookRentalRepository>().To<BookRentalRepositoryMock>();
            Bind<IDomainRepository>().To<DomainRepositoryMock>();
            Bind<IEmployeeRepository>().To<EmployeeRepositoryMock>();
            Bind<IPersonalInfoRepository>().To<PersonalInfoRepositoryMock>();
            Bind<IPublisherRepository>().To<PublisherRepositoryMock>();
            Bind<IReaderRepository>().To<ReaderRepositoryMock>();
            Bind<IReaderBookRepository>().To<ReaderBookRepositoryMock>();
        }
    }
}
