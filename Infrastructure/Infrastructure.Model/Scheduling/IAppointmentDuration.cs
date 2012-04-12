// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAppointmentDuration.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Interface for appointments that have a duration
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Scheduling
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Interface for appointments that have a duration
    /// </summary>
    public interface IAppointmentDuration
    {
        /// <summary>
        /// Gets or sets the units used for the length of the appointment
        /// </summary>
        ScheduleUnits? AppointmentLengthUnits { get; set; }

        /// <summary>
        /// Gets or sets the length of the appointment
        /// </summary>
        decimal? Length { get; set; }
    }
}
