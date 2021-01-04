﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Interfaces.DataAccess;

namespace LibraryAdministration.BusinessLayer
{
    public abstract class BaseService<T, U> : IService<T>
        where T : class
        where U : IRepository<T>
    {
        protected U _repository;
        protected IValidator<T> _validator;

        /// <summary>
        /// Ctor for base service
        /// </summary>
        /// <param name="repository">The actual repository that will fit into this class</param>
        /// <param name="validator">The fluent validation validator</param>
        public BaseService(U repository, IValidator<T> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public ValidationResult Insert(T entity)
        {
            var result = _validator.Validate(entity);
            if (result.IsValid)
            {
                _repository.Insert(entity);
            }

            return result;
        }

        public ValidationResult Update(T entity)
        {
            var result = _validator.Validate(entity);
            if (result.IsValid)
            {
                _repository.Update(entity);
            }

            return result;
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public T GetById(object id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.Get();
        }
    }
}
