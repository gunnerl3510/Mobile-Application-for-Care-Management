// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EfRepository.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Creates a repository based on an EF implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Data.Repository.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Infrastructure.Logging;
    using Infrastructure.Model;

    using Ninject;

    /// <summary>
    /// Creates a repository based on an EF implementation
    /// </summary>
    /// <typeparam name="T">
    /// The <seealso cref="IModel"/> type tha the repository is designed for
    /// </typeparam>
    public sealed class EfRepository<T> : IDisposable, IRepository<T>, IReadOnlyRepository<T>
        where T : IModel, new()
    {
        #region private members

        /// <summary>
        /// The EF container object
        /// </summary>
        private CareManagementContainer container;

        /// <summary>
        /// The <seealso cref="ILogger{T}"/> to use for logging
        /// </summary>
        private ILogger<EfRepository<T>> logger;

        #endregion

        #region constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepository{T}"/> class
        /// </summary>
        /// <param name="logger">
        /// The <seealso cref="ILogger{T}"/> object to use for logging
        /// </param>
        public EfRepository(ILogger<EfRepository<T>> logger)
        {
            container = new CareManagementContainer();

            this.logger = logger;
        }

        #endregion

        #region public static properties

        /// <summary>
        /// Gets or sets the logging client
        /// </summary>
        public static ILogger<EfRepository<T>> Log { get; set; }

        #endregion

        #region Implementation of IRepository<T>

        /// <summary>
        /// Adds an entity to the repository
        /// </summary>
        /// <param name="model">
        /// The entity to add to the repository
        /// </param>
        /// <returns>
        /// The <seealso cref="IModel"/> after begin added to the underlying repository
        /// </returns>
        public T Add(T model)
        {
            var modelAsList = new List<T> { model }.AsEnumerable();

            return Add(modelAsList).Single();
        }

        /// <summary>
        /// Adds an <c>IEnumerable</c> collection of entities to the repository
        /// </summary>
        /// <param name="models">
        /// The collection of entities to add to the repository
        /// </param>
        /// <returns>
        /// The <seealso cref="IEnumerable{T}"/> collection that was added to the repository
        /// </returns>
        public IEnumerable<T> Add(IEnumerable<T> models)
        {
            return ModelToEntityMapper<T>.UpsertMapper[typeof(T)](container, models);
        }

        /// <summary>
        /// Updates a single entity that exists in the repository
        /// </summary>
        /// <param name="model">The entity to update with the new values</param>
        public void Update(T model)
        {
            var models = new List<T> { model }.AsEnumerable();

            ModelToEntityMapper<T>.UpsertMapper[typeof(T)](container, models);
        }

        /// <summary>
        /// Deletes a single entity from the repository
        /// </summary>
        /// <param name="model">The entity to delete</param>
        public void Delete(T model)
        {
            var models = new List<T> { model }.AsEnumerable();
            this.Delete(models);
        }

        /// <summary>
        /// Deletes a collection of entities from the repository
        /// </summary>
        /// <param name="models">The collection of entities to remove from
        /// the repository</param>
        public void Delete(IEnumerable<T> models)
        {
            ModelToEntityMapper<T>.DeleteMapper[typeof(T)](container, models);
        }

        #endregion

        #region Implementation of IReadOnlyRepository<T>

        /// <summary>
        /// Retrieves all instances of <typeparamref name="T"/>
        /// from the repository
        /// </summary>
        /// <returns>An <c>IQueryable</c> collection of type
        /// <typeparamref name="T"/></returns>
        public IQueryable<T> All()
        {
            try
            {
                return EntityToModelMapper<T>.Mapper[typeof(T)](container);
            }
            catch (Exception exception)
            {
                Log.LogException(exception);
            }

            return Enumerable.Empty<T>().AsQueryable();
        }

        /// <summary>
        /// Retrieves a single instance of <typeparamref name="T"/>
        /// from the repository that satisfies the passed in expression
        /// </summary>
        /// <param name="expression">
        /// The algorithm to use to identify the single instance of 
        /// <typeparamref name="T"/> to return
        /// </param>
        /// <returns>
        /// A single instance of <typeparamref name="T"/> or null if the
        /// instance is not found
        /// </returns>
        public T FindBy(Expression<Func<T, bool>> expression)
        {
            try
            {
                return this.FilterBy(expression).SingleOrDefault();
            }
            catch (Exception exception)
            {
                Log.LogException(exception);
            }

            return default(T);
        }

        /// <summary>
        /// Retrieves a set of <typeparamref name="T"/> from the repository
        /// that satisifies the passed in expression.
        /// </summary>
        /// <param name="expression">The algorithm to use to identify the
        /// set of <typeparamref name="T"/> to return</param>
        /// <returns>An <c>IQueryable</c> collection of type <typeparamref name="T"/>
        /// </returns>
        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            try
            {
                return this.All().Where(expression);
            }
            catch (Exception exception)
            {
                Log.LogException(exception);
            }

            return Enumerable.Empty<T>().AsQueryable();
        }

        #endregion

        #region IDisposable implementation

        /// <summary>
        /// The Dispose implementation for the <seealso cref="IDisposable"/> interface
        /// </summary>
        public void Dispose()
        {
            container.Dispose();
        }

        #endregion
    }
}
