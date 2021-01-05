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
    public class ReaderService : BaseService<Reader, IReaderRepository>, IReaderService
    {
        public ReaderService(LibraryContext context)
            : base(new ReaderRepository(context), new ReaderValidator())
        {

        }
    }
}
