using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IRepository<T>
    {
        void Insert(T entity);

        void Update(T item);

        void Delete(T entity);

        T GetById(object id);

        IEnumerable<T> GetAll();
    }
}
