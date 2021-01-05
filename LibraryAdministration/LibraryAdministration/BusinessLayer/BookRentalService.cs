using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataAccessLayer;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;
using LibraryAdministration.Startup;
using LibraryAdministration.Validators;

namespace LibraryAdministration.BusinessLayer
{
    public class BookRentalService : BaseService<BookRental, IBookRentalRepository>, IBookRentalService
    {
        public BookRentalService(LibraryContext context)
            : base(new BookRentalRepository(context), new BookRentalValidator())
        {

        }
    }
}
