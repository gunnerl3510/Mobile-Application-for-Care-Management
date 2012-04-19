// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EfEntityHelpers.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Generic class to assist with EF data manipulation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Data.Repository.EF
{
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Generic class to assist with EF data manipulation
    /// </summary>
    /// <typeparam name="T">
    /// The type of the EF object
    /// </typeparam>
    internal static class EfEntityHelpers<T>
        where T : EntityObject
    {
        /// <summary>
        /// A generic helper method to upsert an entity into the repository
        /// </summary>
        /// <param name="objectSet">
        /// The <seealso cref="ObjectSet{T}"/> to upsert into.
        /// </param>
        /// <param name="entity">
        /// The <typeparamref name="T"/> to upsert
        /// </param>
        public static void Upsert(ObjectSet<T> objectSet, T entity)
        {
            var context = objectSet.Context;
            var key = context.CreateEntityKey(objectSet.EntitySet.Name, entity);

            object currenTInDb;
            if (context.TryGetObjectByKey(key, out currenTInDb))
            {
                context.ApplyCurrentValues(key.EntitySetName, entity);
            }
            else
            {
                objectSet.AddObject(entity);
            }

            context.SaveChanges();
        }

        /// <summary>
        /// A generic helper method to delete an entity in the repository
        /// </summary>
        /// <param name="objectSet">
        /// The <seealso cref="ObjectSet{T}"/> that contains the entities to delete
        /// </param>
        /// <param name="entity">
        /// The <typeparamref name="T"/> to delete.
        /// </param>
        public static void Delete(ObjectSet<T> objectSet, T entity)
        {
            var context = objectSet.Context;
            var key = context.CreateEntityKey(objectSet.EntitySet.Name, entity);

            object currentEntityInDb;
            if (context.TryGetObjectByKey(key, out currentEntityInDb))
            {
                var objectToDelete = (T)currentEntityInDb;
                objectSet.DeleteObject(objectToDelete);
            }

            context.SaveChanges();
        }
    }
}
