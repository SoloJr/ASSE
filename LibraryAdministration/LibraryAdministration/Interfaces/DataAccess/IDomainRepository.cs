using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IDomainRepository : IRepository<Domain>
    {
        IEnumerable<Domain> GetAllParentDomains(int domainId);

        bool CheckDomainConstraint(List<Domain> domains);
    }
}
