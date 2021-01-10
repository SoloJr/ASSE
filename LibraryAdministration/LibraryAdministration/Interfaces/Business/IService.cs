using FluentValidation.Results;
using System.Collections.Generic;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IService<T>
        where T : class
    {
        ValidationResult Insert(T entity);

        ValidationResult Update(T entity);

        void Delete(T entity);

        T GetById(object id);

        IEnumerable<T> GetAll();
    }
}
