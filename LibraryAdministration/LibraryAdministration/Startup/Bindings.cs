//----------------------------------------------------------------------
// <copyright file="Bindings.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Startup
{
    using BusinessLayer;
    using DataAccessLayer;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using Ninject.Modules;

    /// <summary>
    /// Create Bindings
    /// </summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    class Bindings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.LoadRepositoryLayer();
            this.LoadServiceLayer();
        }

        /// <summary>
        /// Loads the service layer.
        /// </summary>
        private void LoadServiceLayer()
        {
            Bind<IAuthorService>().To<AuthorService>();
            Bind<IBookService>().To<BookService>();
            Bind<IBookPublisherService>().To<BookPublisherService>();
            Bind<IDomainService>().To<DomainService>();
            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<IPersonalInfoService>().To<PersonalInfoService>();
            Bind<IPublisherService>().To<PublisherService>();
            Bind<IReaderService>().To<ReaderService>();
            Bind<IReaderBookService>().To<ReaderBookService>();
        }

        /// <summary>
        /// Loads the repository layer.
        /// </summary>
        private void LoadRepositoryLayer()
        {
            Bind<IAuthorRepository>().To<AuthorRepository>();
            Bind<IBookRepository>().To<BookRepository>();
            Bind<IBookPublisherRepository>().To<BookPublisherRepository>();
            Bind<IDomainRepository>().To<DomainRepository>();
            Bind<IEmployeeRepository>().To<EmployeeRepository>();
            Bind<IPersonalInfoRepository>().To<PersonalInfoRepository>();
            Bind<IPublisherRepository>().To<PublisherRepository>();
            Bind<IReaderRepository>().To<ReaderRepository>();
            Bind<IReaderBookService>().To<ReaderBookService>();
        }
    }
}
