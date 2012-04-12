// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log4NetLogger.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   A log4net implementation of the ILogger interface
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Logging.Log4Net
{
    using System;
    using System.Globalization;
    using log4net;

    /// <summary>
    /// A log4net implementation of the ILogger interface
    /// </summary>
    /// <typeparam name="T">
    /// The type of the Log4NetLogger to create
    /// </typeparam>
    public class Log4NetLogger<T> : ILogger<T> where T : class
    {
        #region private members

        /// <summary>
        /// The log4net logger
        /// </summary>
        private static ILog log;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the Log4NetLogger class
        /// </summary>
        public Log4NetLogger()
        {
            log = LogManager.GetLogger(typeof(T));
            GlobalContext.Properties["host"] = Environment.MachineName;
        }

        #endregion

        #region Implementation of ILogger

        /// <summary>
        /// Logs messages related to entering a method body.  Usually
        /// used for debugging purposes.
        /// </summary>
        /// <param name="methodName">The name of the method to log.</param>
        public void EnterMethod(string methodName)
        {
            if (log.IsDebugEnabled)
            {
                log.InfoFormat(CultureInfo.InvariantCulture, "Entering method {0}", methodName);
            }
        }

        /// <summary>
        /// Logs messages related to leaving a method body.  Usually
        /// used for debugging purposes.
        /// </summary>
        /// <param name="methodName">The name of the method to log.</param>
        public void LeaveMethod(string methodName)
        {
            if (log.IsDebugEnabled)
            {
                log.InfoFormat(CultureInfo.InvariantCulture, "Leaving method {0}", methodName);
            }
        }

        /// <summary>
        /// Logs exception messages.
        /// </summary>
        /// <param name="exception">The <c>Exception</c> to log.</param>
        public void LogException(Exception exception)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(string.Format(CultureInfo.InvariantCulture, "{0}", exception.Message), exception);
            }
        }

        /// <summary>
        /// Logs exception messages.
        /// </summary>
        /// <param name="exception">The <c>Exception</c> to log.</param>
        /// <param name="message">The additional message to log with the exception.</param>
        public void LogExceptionWithMessage(Exception exception, string message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(string.Format(CultureInfo.InvariantCulture, "{0}", message), exception);
            }
        }

        /// <summary>
        /// Logs error messages.  These are usually used when a validation
        /// has failed.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void LogError(string message)
        {
            if (log.IsErrorEnabled)
            {
                log.ErrorFormat(CultureInfo.InvariantCulture, "{0}", message);
            }
        }

        /// <summary>
        /// Logs warning messages.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void LogWarningMessage(string message)
        {
            if (log.IsWarnEnabled)
            {
                log.WarnFormat(CultureInfo.InvariantCulture, "{0}", message);
            }
        }

        /// <summary>
        /// Logs information messages.
        /// </summary>
        /// <param name="message">The information message to log.</param>
        public void LogInfoMessage(string message)
        {
            if (log.IsInfoEnabled)
            {
                log.InfoFormat(CultureInfo.InvariantCulture, "{0}", message);
            }
        }

        #endregion
    }
}
