using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using Moq;
using Ninject.Modules;

namespace LibraryAdministrationTest.Mocks
{
    internal class MockBindings : NinjectModule
    {
        public override void Load()
        {

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
            Bind<IAuthorRepository>().To<AuthorRepository>();
            Bind<IBookRepository>().To<BookRepository>();
            Bind<IBookPublisherRepository>().To<BookPublisherRepository>();
            Bind<IBookRentalRepository>().To<BookRentalRepository>();
            Bind<IDomainRepository>().To<DomainRepository>();
            Bind<IEmployeeRepository>().To<EmployeeRepository>();
            Bind<IPersonalInfoRepository>().To<PersonalInfoRepository>();
            Bind<IPublisherRepository>().To<PublisherRepository>();
            Bind<IReaderRepository>().To<ReaderRepository>();
            Bind<IReaderBookRepository>().To<ReaderBookRepository>();
        }
    }
}
