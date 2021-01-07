using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

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
