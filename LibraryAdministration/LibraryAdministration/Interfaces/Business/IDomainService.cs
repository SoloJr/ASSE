using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IDomainService : IService<Domain>
    {
        /// <summary>
        /// Gets all domains (including parents) of book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        IEnumerable<Domain> GetAllDomainsOfBook(int bookId);
    }
}
