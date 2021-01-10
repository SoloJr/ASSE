//-----------------------------------------------------------------------
// <copyright file="BookRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using Interfaces.DataAccess;

    /// <summary>
    /// Book Repository repository
    /// </summary>
    /// <seealso cref="LibraryAdministration.DataAccessLayer.BaseRepository{LibraryAdministration.DomainModel.Book}" />
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IBookRepository" />
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BookRepository(LibraryContext context) 
            : base(context)
        {
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BookRepository"/> class.
        /// </summary>
        ~BookRepository()
        {
            Context.Dispose();
        }

        /// <summary>
        /// Gets all domains of book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>All domains of a book</returns>
        /// <exception cref="ArgumentNullException">bookId is not correct</exception>
        public IEnumerable<Domain> GetAllDomainsOfBook(int bookId)
        {
            var list = new List<Domain>();

            var book = Context.Books.FirstOrDefault(x => x.Id == bookId) ?? throw new ArgumentNullException();
            list.AddRange(book.Domains);
            for (var i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).ParentId == null)
                {
                    continue;
                }

                var id = list.ElementAt(i).ParentId;
                var newDom = Context.Domains.FirstOrDefault(x => x.Id == id);
                list.Add(newDom);
            }

            return list;
        }
    }
}
