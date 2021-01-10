using LibraryAdministration.DomainModel;
using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IDomainService : IService<Domain>
    {
        IEnumerable<Domain> GetAllParentDomains(int domainId);

        bool CheckDomainConstraint(List<Domain> domains);
    }
}
