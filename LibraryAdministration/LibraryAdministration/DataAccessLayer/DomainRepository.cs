using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.DataAccess;
namespace LibraryAdministration.DataAccessLayer
{
    public class DomainRepository : BaseRepository<Domain>, IDomainRepository
    {
        public DomainRepository(LibraryContext context) : base(context) { }

        ~DomainRepository()
        {
            _context.Dispose();
        }

        public IEnumerable<Domain> GetAllParentDomains(int domainId)
        {
            var list = new List<Domain>();

            var domain = _context.Domains.FirstOrDefault(x => x.Id == domainId) ??
                        throw new ArgumentNullException("Domain Not Found");

            while (domain != null)
            {
                var parId = domain.ParentId ?? 0;
                domain = _context.Domains.FirstOrDefault(x => x.Id == parId);
                if (domain != null)
                {
                    list.Add(domain);
                }
            }

            return list;
        }

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
                    iterator = _context.Domains.FirstOrDefault(x => x.Id == parId);
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
