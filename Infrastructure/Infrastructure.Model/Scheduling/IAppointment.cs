// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAppointment.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Interface to encapsulate required appointment data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Scheduling
{
    using System;

    /// <summary>
    /// Interface to encapsulate required appointment data
    /// </summary>
    public interface IAppointment
    {
        /// <summary>
        /// Gets or sets the date and time of the appointment in UTC time
        /// </summary>
        DateTimeOffset AppointmentDateTimeUtc { get; set; }
    }
}
