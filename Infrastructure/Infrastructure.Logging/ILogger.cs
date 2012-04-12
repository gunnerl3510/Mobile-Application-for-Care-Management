// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILogger.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   The logging interface that all loggers will implement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging
{
    using System;

    /// <summary>
    /// The logging interface that all loggers will implement
    /// </summary>
    /// <typeparam name="T">
    /// The object type of the logger
    /// </typeparam>
    public interface ILogger<T> where T : class
    {
        /// <summary>
        /// Logs messages related to entering a method body.  Usually
        /// used for debugging purposes.
        /// </summary>
        /// <param name="methodName">The name of the method to log.</param>
        void EnterMethod(string methodName);

        /// <summary>
        /// Logs messages related to leaving a method body.  Usually
        /// used for debugging purposes.
        /// </summary>
        /// <param name="methodName">The name of the method to log.</param>
        void LeaveMethod(string methodName);

        /// <summary>
        /// Logs exception messages.
        /// </summary>
        /// <param name="exception">The <c>Exception</c> to log.</param>
        void LogException(Exception exception);

        /// <summary>
        /// Logs exception messages.
        /// </summary>
        /// <param name="exception">The <c>Exception</c> to log.</param>
        /// <param name="message">The additional message to log with the exception.</param>
        void LogExceptionWithMessage(Exception exception, string message);

        /// <summary>
        /// Logs error messages.  These are usually used when a validation
        /// has failed.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        void LogError(string message);

        /// <summary>
        /// Logs warning messages.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        void LogWarningMessage(string message);

        /// <summary>
        /// Logs information messages.
        /// </summary>
        /// <param name="message">The information message to log.</param>
        void LogInfoMessage(string message);
    }
}
