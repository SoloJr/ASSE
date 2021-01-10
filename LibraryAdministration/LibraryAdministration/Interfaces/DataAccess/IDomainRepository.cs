using LibraryAdministration.DomainModel;
using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IDomainRepository : IRepository<Domain>
    {
        IEnumerable<Domain> GetAllParentDomains(int domainId);

        bool CheckDomainConstraint(List<Domain> domains);
    }
}
