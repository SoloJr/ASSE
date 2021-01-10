//-----------------------------------------------------------------------
// <copyright file="DomainService.cs" company="Transilvania University of Brasov">
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
    /// Domain Service class
    /// </summary>
    /// <seealso cref="LibraryAdministration.BusinessLayer.BaseService{LibraryAdministration.DomainModel.Domain, LibraryAdministration.Interfaces.DataAccess.IDomainRepository}" />
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IDomainService" />
    public class DomainService : BaseService<Domain, IDomainRepository>, IDomainService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DomainService(LibraryContext context)
            : base(new DomainRepository(context), new DomainValidator())
        {
        }

        /// <summary>
        /// Gets all parent domains.
        /// </summary>
        /// <param name="domainId">The domain identifier.</param>
        /// <returns>All parent domains of a domain</returns>
        /// <exception cref="LibraryArgumentException">domainId is wrong</exception>
        public IEnumerable<Domain> GetAllParentDomains(int domainId)
        {
            if (domainId <= 0)
            {
                throw new LibraryArgumentException(nameof(domainId));
            }

            return Repository.GetAllParentDomains(domainId);
        }

        /// <summary>
        /// Checks the domain constraint.
        /// </summary>
        /// <param name="domains">The domains.</param>
        /// <returns>boolean value</returns>
        /// <exception cref="LibraryArgumentException">domains is null</exception>
        public bool CheckDomainConstraint(List<Domain> domains)
        {
            if (domains == null)
            {
                throw new LibraryArgumentException(nameof(domains));
            }

            return Repository.CheckDomainConstraint(domains);
        }
    }
}
