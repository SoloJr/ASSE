//-----------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.BusinessLayer
{
    using FluentValidation;
    using FluentValidation.Results;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using System.Collections.Generic;

    public abstract class BaseService<T, U> : IService<T>
        where T : class
        where U : IRepository<T>
    {
        protected U _repository;
        protected IValidator<T> _validator;
        /// <summary>
        /// Constructor for base service
        /// </summary>
        /// <param name="repository">The actual repository that will fit into this class</param>
        /// <param name="validator">The fluent validation validator</param>
        public BaseService(U repository, IValidator<T> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public ValidationResult Insert(T entity)
        {
            var result = _validator.Validate(entity);
            if (result.IsValid)
            {
                _repository.Insert(entity);
            }

            return result;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public ValidationResult Update(T entity)
        {
            var result = _validator.Validate(entity);
            if (result.IsValid)
            {
                _repository.Update(entity);
            }

            return result;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetById(object id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
