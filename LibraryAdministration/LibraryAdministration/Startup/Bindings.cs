using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.BusinessLayer;
using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using Ninject;
using Ninject.Extensions.Logging;
using Ninject.Modules;

namespace LibraryAdministration.Startup
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            LoadRepositoryLayer();
            LoadServiceLayer();
        }

        private void LoadServiceLayer()
        {
            Bind<IBookService>().To<BookService>();
        }

        private void LoadRepositoryLayer()
        {
            Bind<IBookRepository>().To<BookRepository>();
        }
    }
}
