using System;
using System.Collections.Generic;
using System.Configuration;
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

        public List<ReaderBook> GetAllBooksOnLoan(int readerId)
        {
            var per = ConfigurationManager.AppSettings["PER"];
            var perInt = int.Parse(per);
            using (_context)
            {
                var date = DateTime.Now.AddDays(-perInt);
                return _context.ReaderBooks.Where(x => x.ReaderId == readerId && x.LoanDate >= date).ToList();
            }
        }
    }
}
