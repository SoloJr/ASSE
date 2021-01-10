using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Helper;
using LibraryAdministration.Interfaces.DataAccess;
namespace LibraryAdministration.DataAccessLayer
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {
        }

        ~BookRepository()
        {
            _context.Dispose();
        }

        public IEnumerable<Domain> GetAllDomainsOfBook(int bookId)
        {
            var list = new List<Domain>();

            var book = _context.Books.FirstOrDefault(x => x.Id == bookId) ?? throw new ArgumentNullException();
            list.AddRange(book.Domains);
            for (var i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).ParentId == null) continue;

                var id = list.ElementAt(i).ParentId;
                var newDom = _context.Domains.FirstOrDefault(x => x.Id == id);
                list.Add(newDom);
            }

            return list;
        }
    }
}
