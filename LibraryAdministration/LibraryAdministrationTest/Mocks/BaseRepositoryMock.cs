using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.DataAccess;

namespace LibraryAdministrationTest.Mocks
{
    internal abstract class BaseRepositoryMock<T> : IRepository<T>
        where T : class
    {
        public void Insert(T entity)
        {
            // we can consider that it was inserted
        }

        public void Update(T item)
        {
            // we can consider that it was updated
        }

        public void Delete(T entity)
        {
            throw new DeleteItemException();
            // we can consider that it was deleted
        }

        public T GetById(int id)
        {
            return null;
        }

        public IEnumerable<T> GetAll()
        {
            return null;
        }
    }
}