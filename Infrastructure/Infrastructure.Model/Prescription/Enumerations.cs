// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enumerations.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Enumerations for the prescription models
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Prescription
{
    /// <summary>
    /// Base medicine dosage units
    /// </summary>
    public enum DosageUnits
    {
        /// <summary>
        /// Dosages measured in tablets or pills
        /// </summary>
        Tablet = 1,

        /// <summary>
        /// Dosages measured in milliliters
        /// </summary>
        Milliliter = 2,

        /// <summary>
        /// Dosages measured in milligrams
        /// </summary>
        Milligrams = 3,

        /// <summary>
        /// Dosages measured in Teaspoons
        /// </summary>
        Teaspoon = 4,

        /// <summary>
        /// Dosages measured in Tablespoons
        /// </summary>
        Tablespoon = 5
    }
}
