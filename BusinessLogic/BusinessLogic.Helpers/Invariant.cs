// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Invariant.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Helper class to handle argument validation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLogic.Helpers
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Helper class to handle argument validation
    /// </summary>
    public class Invariant
    {
        /// <summary>
        /// Test to ensure that the paramater identified is not null
        /// </summary>
        /// <param name="target">The parameter to null test</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <exception cref="ArgumentNullException">Thrown if the parameter
        /// to test is null</exception>
        public static void IsNotNull(object target, string parameterName)
        {
            if (target == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Test to ensure that a string parameter is not null or empty string
        /// </summary>
        /// <param name="target">The string to test</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <exception cref="ArgumentException">Thrown if the parameter to test
        /// is null or an empty string</exception>
        public static void IsNotBlank(string target, string parameterName)
        {
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, "\"{0}\" cannot be blank.", parameterName));
            }
        }
    }
}