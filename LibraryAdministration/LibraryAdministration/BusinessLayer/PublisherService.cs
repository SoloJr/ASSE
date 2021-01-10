//-----------------------------------------------------------------------
// <copyright file="PublisherService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.BusinessLayer
{
    using System.Collections.Generic;
    using DataAccessLayer;
    using DataMapper;
    using DomainModel;
    using Helper;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using Validators;

    /// <summary>
    /// Publisher Service class
    /// </summary>
    /// <seealso cref="LibraryAdministration.BusinessLayer.BaseService{LibraryAdministration.DomainModel.Publisher, LibraryAdministration.Interfaces.DataAccess.IPublisherRepository}" />
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IPublisherService" />
    public class PublisherService : BaseService<Publisher, IPublisherRepository>, IPublisherService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PublisherService(LibraryContext context)
            : base(new PublisherRepository(context), new PublisherValidator())
        {
        }

        /// <summary>
        /// Gets all book publishers of a book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>All publishers of a book</returns>
        /// <exception cref="LibraryArgumentException">bookId is wrong</exception>
        public ICollection<Publisher> GetAllBookPublishersOfABook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookId));
            }

            return Repository.GetAllBookPublishersOfABook(bookId);
        }
    }
}
