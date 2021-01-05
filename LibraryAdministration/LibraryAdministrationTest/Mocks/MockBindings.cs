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
            Bind<IBookPublisherService>().To<BookPublisherServiceMock>();
            Bind<IBookRentalService>().To<BookRentalServiceMock>();
            Bind<IDomainService>().To<DomainServiceMock>();
            Bind<IEmployeeService>().To<EmployeeServiceMock>();
            Bind<IPersonalInfoService>().To<PersonalInfoServiceMock>();
            Bind<IPublisherService>().To<PublisherServiceMock>();
            Bind<IReaderService>().To<ReaderServiceMock>();
            Bind<IReaderBookService>().To<ReaderBookServiceMock>();
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
