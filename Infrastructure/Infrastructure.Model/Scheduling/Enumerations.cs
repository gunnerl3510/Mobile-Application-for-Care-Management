// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enumerations.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Enumerations that apply to appointment models
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Scheduling
{
    /// <summary>
    /// The unit used to determine the length of the appointment
    /// </summary>
    public enum ScheduleUnits
    {
        /// <summary>
        /// Scheduled time is measured in minutes
        /// </summary>
        Minutes = 1,

        /// <summary>
        /// Schedule time is measured in hours
        /// </summary>
        Hours = 2
    }
}
