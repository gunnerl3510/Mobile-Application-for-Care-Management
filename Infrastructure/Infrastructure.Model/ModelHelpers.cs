// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelHelpers.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Extension methods for <seealso cref="IModel" /> instances
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model
{
    using Infrastructure.Helpers;

    /// <summary>
    /// Extension methods for <seealso cref="IModel"/> instances
    /// </summary>
    public class ModelHelpers
    {
        /// <summary>
        /// Retrieves the default <seealso cref="StrictKeyEqualityComparer{T, int}"/>
        /// for an <seealso cref="IModel"/>
        /// </summary>
        /// <typeparam name="T">
        /// A type that implements <seealso cref="IModel"/>
        /// </typeparam>
        /// <returns>
        /// A default <seealso cref="StrictKeyEqualityComparer{T, int}"/> for the
        /// <seealso cref="IModel"/> instance
        /// </returns>
        public static StrictKeyEqualityComparer<T, int> Comparer<T>() where T : IModel
        {
            return new StrictKeyEqualityComparer<T, int>(modelObject => modelObject.Id);
        }
    }
}
