//----------------------------------------------------------------------
// <copyright file="IService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Interfaces.Business
{
    using System.Collections.Generic;
    using FluentValidation.Results;

    /// <summary>
    /// Interface for generic service
    /// </summary>
    /// <typeparam name="T">DomainModel class</typeparam>
    public interface IService<T>
        where T : class
    {
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Validation Result</returns>
        ValidationResult Insert(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Validation Result</returns>
        ValidationResult Update(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The object</returns>
        T GetById(object id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>All the objects</returns>
        IEnumerable<T> GetAll();
    }
}
