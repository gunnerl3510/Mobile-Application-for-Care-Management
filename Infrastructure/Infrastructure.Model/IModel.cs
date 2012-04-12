// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModel.cs" company="LC LLC">
//   All rights reserved LC LLC 2012
// </copyright>
// <summary>
//   Interface used for typing of all models in the Model namespace
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model
{
    /// <summary>
    /// Interface used for typing of all models in the Model namespace
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for this <see cref="IModel"/>
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the version identifier for the current record
        /// </summary>
        byte[] CurrentVersion { get; set; }
    }
}
