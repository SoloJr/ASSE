//-----------------------------------------------------------------------
// <copyright file="DomainRepository.cs" company="Transilvania University of Brasov">
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
    /// Domain Repository class
    /// </summary>
    /// <seealso cref="LibraryAdministration.DataAccessLayer.BaseRepository{LibraryAdministration.DomainModel.Domain}" />
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IDomainRepository" />
    public class DomainRepository : BaseRepository<Domain>, IDomainRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DomainRepository(LibraryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DomainRepository"/> class.
        /// </summary>
        ~DomainRepository()
        {
            Context.Dispose();
        }

        /// <summary>
        /// Gets all parent domains.
        /// </summary>
        /// <param name="domainId">The domain identifier.</param>
        /// <returns>All parent domains</returns>
        /// <exception cref="ArgumentNullException">Domain Not Found</exception>
        public IEnumerable<Domain> GetAllParentDomains(int domainId)
        {
            var list = new List<Domain>();

            var domain = Context.Domains.FirstOrDefault(x => x.Id == domainId) ??
                        throw new ArgumentNullException("Domain Not Found");

            while (domain != null)
            {
                var parId = domain.ParentId ?? 0;
                domain = Context.Domains.FirstOrDefault(x => x.Id == parId);
                if (domain != null)
                {
                    list.Add(domain);
                }
            }

            return list;
        }

        /// <summary>
        /// Checks the domain constraint.
        /// </summary>
        /// <param name="domains">The domains.</param>
        /// <returns>boolean value</returns>
        public bool CheckDomainConstraint(List<Domain> domains)
        {
            var hash = new HashSet<int>();
            domains.ForEach(x => hash.Add(x.Id));

            foreach (var domain in domains)
            {
                var iterator = domain;
                while (iterator != null)
                {
                    var parId = iterator.ParentId ?? 0;
                    iterator = Context.Domains.FirstOrDefault(x => x.Id == parId);
                    if (iterator != null && hash.Contains(iterator.Id))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
