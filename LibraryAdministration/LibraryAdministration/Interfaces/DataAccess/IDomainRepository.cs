//---------------------------------------------------------
// <copyright file="IDomainRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.DataAccess
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IDomainRepository interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{LibraryAdministration.DomainModel.Domain}" />
    public interface IDomainRepository : IRepository<Domain>
    {
        /// <summary>
        /// Gets all parent domains.
        /// </summary>
        /// <param name="domainId">The domain identifier.</param>
        /// <returns>all parent domains</returns>
        IEnumerable<Domain> GetAllParentDomains(int domainId);

        /// <summary>
        /// Checks the domain constraint.
        /// </summary>
        /// <param name="domains">The domains.</param>
        /// <returns>boolean value</returns>
        bool CheckDomainConstraint(List<Domain> domains);
    }
}
