// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyEqualityComparer.cs" company="LC LLC">
//   All rights reserved LC LLC 2012
// </copyright>
// <summary>
//   An implementation of the <see cref="IEqualityComparer{T}" />
//   interface.  Used for lambda expression to test model keys for equality.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An implementation of the <see cref="IEqualityComparer{T}"/>
    /// interface.  Used for lambda expression to test model keys for equality.
    /// </summary>
    /// <typeparam name="T">The type of the object to compare</typeparam>
    /// <typeparam name="TKey">The type of the object's key</typeparam>
    public class KeyEqualityComparer<T, TKey> : IEqualityComparer<T>
    {
        /// <summary>
        /// The <see cref="Func{T, TKey}"/> to use for comparing two objects
        /// </summary>
        protected readonly Func<T, TKey> KeyExtractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEqualityComparer{T,TKey}"/> class.
        /// </summary>
        /// <param name="keyExtractor">
        /// The key extractor <see cref="Func{T, TKey}" /> to use to retrieve the key from the object
        /// </param>
        public KeyEqualityComparer(Func<T, TKey> keyExtractor)
        {
            this.KeyExtractor = keyExtractor;
        }

        /// <summary>
        /// Tests two objects for equality
        /// </summary>
        /// <param name="x">
        /// The first object to compare
        /// </param>
        /// <param name="y">
        /// The object to compare to the first object
        /// </param>
        /// <returns>
        /// A value that determines whether the two objects ar the same
        /// object based on their key values
        /// </returns>
        public virtual bool Equals(T x, T y)
        {
            return this.KeyExtractor(x).Equals(this.KeyExtractor(y));
        }

        /// <summary>
        /// Generates a hash code for the <see cref="Func{T, TKey}"/>
        /// used to compare two objects of type T
        /// </summary>
        /// <param name="obj">
        /// The object to generate a hash code for
        /// </param>
        /// <returns>
        /// The hash code as an integer
        /// </returns>
        public int GetHashCode(T obj)
        {
            return this.KeyExtractor(obj).GetHashCode();
        }
    }
}
