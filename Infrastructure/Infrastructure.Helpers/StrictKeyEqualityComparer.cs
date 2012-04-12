// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StrictKeyEqualityComparer.cs" company="LC LLC">
//   All rights reserved LC LLC 2012
// </copyright>
// <summary>
//   An implementation of the <see cref="KeyEqualityComparer{T, TKey}" />
//   for comparison of value types with a key
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Helpers
{
    using System;

    /// <summary>
    /// An implementation of the <see cref="KeyEqualityComparer{T, TKey}"/>
    /// for comparison of value types with a key
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object to compare
    /// </typeparam>
    /// <typeparam name="TKey">
    /// They type of the key used to compare the two objects
    /// </typeparam>
    public class StrictKeyEqualityComparer<T, TKey> : KeyEqualityComparer<T, TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrictKeyEqualityComparer{T,TKey}"/> class.
        /// </summary>
        /// <param name="keyExtractor">
        /// The key extractor <see cref="Func{T, TKey}"/> to use to retrieve the key from the object
        /// </param>
        public StrictKeyEqualityComparer(Func<T, TKey> keyExtractor)
            : base(keyExtractor)
        {
        }

        /// <summary>
        /// Tests two objects for equality
        /// </summary>
        /// <param name="x">
        /// The first object to compary
        /// </param>
        /// <param name="y">
        /// The object to compare to the first object
        /// </param>
        /// <returns>
        /// A value that determines whether the two objects ar the same
        /// object based on their key values
        /// </returns>
        public override bool Equals(T x, T y)
        {
            // This will use the overload that accepts a TKey parameter
            // instead of an object parameter.
            return this.KeyExtractor(x).Equals(this.KeyExtractor(y));
        }
    }
}
