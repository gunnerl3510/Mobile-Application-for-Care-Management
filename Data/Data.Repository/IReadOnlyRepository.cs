// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IReadOnlyRepository.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Interface for reading from a repository
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Data.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Infrastructure.Model;

    /// <summary>
    /// Interface for reading from a repository
    /// </summary>
    /// <typeparam name="T"> Any type defined in the 
    /// <see cref="Infrastructure.Model"/> namespace
    /// </typeparam>
    public interface IReadOnlyRepository<T>
        where T : IModel
    {
        /// <summary>
        /// Retrieves all instances of <typeparamref name="T"/>
        /// from the repository
        /// </summary>
        /// <returns>An <c>IQueryable</c> collection of type
        /// <typeparamref name="T"/></returns>
        IQueryable<T> All();

        /// <summary>
        /// Retrieves a single instance of <typeparamref name="T"/>
        /// from the repository that satisfies the passed in expression
        /// </summary>
        /// <param name="expression">The algorithm to use to identify the
        /// single instance of <typeparamref name="T"/> to return</param>
        /// <returns>A single instance of <typeparamref name="T"/> or
        /// null if the instance is not found</returns>
        T FindBy(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Retrieves a set of <typeparamref name="T"/> from the repository
        /// that satisifies the passed in expression.
        /// </summary>
        /// <param name="expression">The algorithm to use to identify the
        /// set of <typeparamref name="T"/> to return</param>
        /// <returns>An <c>IQueryable</c> collection of type
        /// <typeparamref name="T"/></returns>
        IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);
    }
}
