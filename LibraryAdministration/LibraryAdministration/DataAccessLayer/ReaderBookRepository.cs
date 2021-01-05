using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.DataAccess;

namespace LibraryAdministration.DataAccessLayer
{
    public class ReaderBookRepository : BaseRepository<ReaderBook>, IReaderBookRepository
    {
        public ReaderBookRepository(LibraryContext context)
            : base(context)
        {
            
        }
    }
}
