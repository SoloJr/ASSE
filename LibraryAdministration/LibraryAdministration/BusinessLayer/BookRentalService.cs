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
    class BookRentalService : BaseService<BookRental, IBookRentalRepository>, IBookRentalService
    {
        public BookRentalService()
            : base(Injector.Get<IBookRentalRepository>(), new BookRentalValidator())
        {

        }
    }
}
