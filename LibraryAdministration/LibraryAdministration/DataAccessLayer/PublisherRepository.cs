//-----------------------------------------------------------------------
// <copyright file="PublisherRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataAccessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using Interfaces.DataAccess;

    /// <summary>
    /// PublisherRepository class
    /// </summary>
    /// <seealso cref="LibraryAdministration.DataAccessLayer.BaseRepository{LibraryAdministration.DomainModel.Publisher}" />
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IPublisherRepository" />
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PublisherRepository(LibraryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all book publishers of a book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>All the publishers of a book</returns>
        public ICollection<Publisher> GetAllBookPublishersOfABook(int bookId)
        {
            var bookPublishers = Context.BookPublisher.Where(x => x.BookId == bookId).ToList();
            var list = new List<Publisher>();

            bookPublishers.ForEach(x => list.Add(x.Publisher));

            return list;
        }
    }
}
