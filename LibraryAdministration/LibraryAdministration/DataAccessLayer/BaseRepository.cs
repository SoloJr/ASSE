﻿//-----------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataAccessLayer
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using DataMapper;
    using Interfaces.DataAccess;
    using Ninject;
    using Ninject.Extensions.Logging;
    using Startup;

    /// <summary>
    /// BaseRepository class
    /// </summary>
    /// <typeparam name="T">Domain Model class</typeparam>
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IRepository{T}" />
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// The this.logger
        /// </summary>
        protected readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected BaseRepository(LibraryContext context)
        {
            this.Context = context;
            var loggerFactory = Injector.Kernel.Get<ILoggerFactory>();
            this.logger = loggerFactory.GetCurrentClassLogger();
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        protected LibraryContext Context { get; set; }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(T entity)
        {
            var set = this.Context.Set<T>();
            set.Add(entity);

            this.Context.SaveChanges();

            this.logger.Info($"Repository: Added an entity in database: {entity}");
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Update(T item)
        {
            var set = this.Context.Set<T>();
            set.Attach(item);
            this.Context.Entry(item).State = EntityState.Modified;

            this.Context.SaveChanges();

            this.logger.Info($"Repository: Updated an entity in database: {item}");
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(object id)
        {
            this.Delete(this.GetById(id));
            this.logger.Info($"Repository: Deleted an entity in database: {id}");
        }

        /// <summary>
        /// Deletes the specified entity to delete.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public virtual void Delete(T entityToDelete)
        {
            var set = this.Context.Set<T>();

            if (this.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                set.Attach(entityToDelete);
            }

            set.Remove(entityToDelete);

            this.Context.SaveChanges();

            this.logger.Info($"Repository: Deleted an entity in database: {entityToDelete}");
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The object
        /// </returns>
        public virtual T GetById(object id)
        {
            this.logger.Info($"Repository: Got an entity by id from database: {id}");
            return this.Context.Set<T>().Find(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// All the objects
        /// </returns>
        public IEnumerable<T> GetAll()
        {
            var set = this.Context.Set<T>();

            return set.ToList();
        }
    }
}
