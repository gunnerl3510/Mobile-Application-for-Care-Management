// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the interface for classes that create, update, or delete
//   from the repository
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Data.Repository
{
    using System.Collections.Generic;
    using Infrastructure.Model;

    /// <summary>
    /// Defines the interface for classes that create, update, or delete
    /// from the repository
    /// </summary>
    /// <typeparam name="T">
    /// Any type implementing <seealso cref="IModel"/> defined in the <see cref="Infrastructure.Model"/> namesapce
    /// </typeparam>
    public interface IRepository<T>
        where T : IModel
    {
        /// <summary>
        /// Adds an entity to the repository
        /// </summary>
        /// <param name="model">
        /// The entity to add to the repository
        /// </param>
        /// <returns>
        /// The <seealso cref="IModel"/> after begin added to the underlying repository
        /// </returns>
        T Add(T model);

        /// <summary>
        /// Adds an <c>IEnumerable</c> collection of entities to the repository
        /// </summary>
        /// <param name="models">
        /// The collection of entities to add to the repository
        /// </param>
        /// <returns>
        /// The <seealso cref="IEnumerable{T}"/> collection that was added to the repository
        /// </returns>
        IEnumerable<T> Add(IEnumerable<T> models);

        /// <summary>
        /// Updates a single entity that exists in the repository
        /// </summary>
        /// <param name="model">The entity to update with the new values</param>
        void Update(T model);

        /// <summary>
        /// Deletes a single entity from the repository
        /// </summary>
        /// <param name="model">The entity to delete</param>
        void Delete(T model);

        /// <summary>
        /// Deletes a collection of entities from the repository
        /// </summary>
        /// <param name="models">The collection of entities to remove from
        /// the repository</param>
        void Delete(IEnumerable<T> models);
    }
}
