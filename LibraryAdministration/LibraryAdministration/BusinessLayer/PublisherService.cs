//-----------------------------------------------------------------------
// <copyright file="PublisherService.cs" company="Transilvania University of Brasov">
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
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">bookId</exception>
        public ICollection<Publisher> GetAllBookPublishersOfABook(int bookId)
        {
            if (bookId <= 0)
            {
                throw new LibraryArgumentException(nameof(bookId));
            }

            return _repository.GetAllBookPublishersOfABook(bookId);
        }
    }
}
