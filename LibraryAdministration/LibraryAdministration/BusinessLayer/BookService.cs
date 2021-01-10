//-----------------------------------------------------------------------
// <copyright file="BookService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.BusinessLayer
{
    using DataAccessLayer;
    using DataMapper;
    using DomainModel;
    using Helper;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using Validators;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="LibraryAdministration.BusinessLayer.BaseService{LibraryAdministration.DomainModel.Book, LibraryAdministration.Interfaces.DataAccess.IBookRepository}" />
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IBookService" />
    public class BookService : BaseService<Book, IBookRepository>, IBookService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BookService(LibraryContext context)
            : base(new BookRepository(context), new BookValidator())
        {

        }

        /// <summary>
        /// Gets all domains of book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">bookId</exception>
        public IEnumerable<Domain> GetAllDomainsOfBook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookId));
            }

            return _repository.GetAllDomainsOfBook(bookId);
        }
    }
}
