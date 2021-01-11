//-----------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.BusinessLayer
{
    using System.Collections.Generic;
    using FluentValidation;
    using FluentValidation.Results;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using Ninject;
    using Ninject.Extensions.Logging;
    using Startup;

    /// <summary>
    /// The base service
    /// </summary>
    /// <typeparam name="T">Service interface implementation</typeparam>
    /// <typeparam name="U">Repository interface implementation</typeparam>
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IService{T}" />
    public abstract class BaseService<T, U> : IService<T>
        where T : class
        where U : IRepository<T>
    {
        /// <summary>
        /// The this.logger
        /// </summary>
        protected readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{T, U}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="validator">The validator.</param>
        public BaseService(U repository, IValidator<T> validator)
        {
            this.Repository = repository;
            this.Validator = validator;
            var loggerFactory = Injector.Kernel.Get<ILoggerFactory>();
            this.logger = loggerFactory.GetCurrentClassLogger();
        }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        protected U Repository { get; set; }

        /// <summary>
        /// Gets or sets the validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        protected IValidator<T> Validator { get; set; }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A validation result</returns>
        public ValidationResult Insert(T entity)
        {
            var result = this.Validator.Validate(entity);
            if (result.IsValid)
            {
                this.Repository.Insert(entity);
            }

            this.logger.Info($"Service: Added an entity in database: {entity}");
            return result;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A validation result</returns>
        public ValidationResult Update(T entity)
        {
            var result = this.Validator.Validate(entity);
            if (result.IsValid)
            {
                this.Repository.Update(entity);
            }

            this.logger.Info($"Service: Updated an entity in database: {entity}");
            return result;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            this.Repository.Delete(entity);
            this.logger.Info($"Service: Updated an entity in database: {entity}");
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The object</returns>
        public T GetById(object id)
        {
            this.logger.Info($"Service: Got an entity by id: {id}");
            return this.Repository.GetById(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>All the available objects</returns>
        public IEnumerable<T> GetAll()
        {
            return this.Repository.GetAll();
        }
    }
}
