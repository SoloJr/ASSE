using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IRepository<T>
    {
        void Insert(T entity);

        void Update(T item);

        void Delete(T entity);

        T GetById(int id);

        IEnumerable<T> GetAll();
    }
}
