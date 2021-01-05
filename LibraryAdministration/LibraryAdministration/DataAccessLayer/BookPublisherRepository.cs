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
    public class BookPublisherRepository : BaseRepository<BookPublisher>, IBookPublisherRepository
    {
        public BookPublisherRepository(LibraryContext context) : base(context) { }
    }
}
