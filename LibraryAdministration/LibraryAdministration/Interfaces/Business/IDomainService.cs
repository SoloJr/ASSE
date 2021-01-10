//----------------------------------------------------------------------
// <copyright file="IDomainService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using System.Collections.Generic;
    using DomainModel;

    /// <summary>
    /// IDomainService interface
    /// </summary>
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IService{LibraryAdministration.DomainModel.Domain}" />
    public interface IDomainService : IService<Domain>
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
