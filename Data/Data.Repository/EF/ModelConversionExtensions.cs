// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelConversionExtensions.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Maintains the model to EF entity conversion mappings
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Data.Repository.EF
{
    using System;
    using System.Data.Objects.DataClasses;

    using AccountModels = Infrastructure.Model.Account;
    using InsuranceModels = Infrastructure.Model.Insurance;
    using MedicalModels = Infrastructure.Model.Medical;
    using PrescriptionModels = Infrastructure.Model.Prescription;

    /// <summary>
    /// Maintains the model to EF entity conversion mappings
    /// </summary>
    internal static class ModelConversionExtensions
    {
        /// <summary>
        /// Converts an <seealso cref="AccountModels.Account"/> to an EF 
        /// <seealso cref="Account"/> entity
        /// </summary>
        /// <param name="account">The <c>Model.Account</c> to convert</param>
        /// <returns>An EF <c>Account</c> <c>EntityObject</c></returns>
        public static Account ToEfAccount(this AccountModels.Account account)
        {
            Guid tempGuid;
            return new Account
                {
                    AccountId = account.Id,
                    CurrentVersion = account.CurrentVersion,
                    EmailAddress = account.EmailAddress,
                    Name = account.Name,
                    UserId = Guid.TryParse(account.UserId, out tempGuid) ? tempGuid : default(Guid)
                };
        }

        /// <summary>
        /// Converts an <seealso cref="InsuranceModels.Insurer"/> to an EF 
        /// <seealso cref="Insurer"/> entity
        /// </summary>
        /// <param name="insurer">
        /// The <seealso cref="InsuranceModels.Insurer"/> to convert
        /// </param>
        /// <returns>
        /// An EF <seealso cref="Insurer"/> <seealso cref="EntityObject"/>
        /// </returns>
        public static Insurer ToEfInsurer(this InsuranceModels.Insurer insurer)
        {
            return new Insurer
                {
                    AccountId = insurer.AccountId,
                    CurrentVersion = insurer.CurrentVersion,
                    InsurerId = insurer.Id,
                    Name = insurer.Name
                };
        }

        /// <summary>
        /// Converts an <seealso cref="InsuranceModels.AuthorizationRequest"/> to an EF 
        /// <seealso cref="AuthorizationRequest"/> entity
        /// </summary>
        /// <param name="request">
        /// The <seealso cref="InsuranceModels.AuthorizationRequest"/> to convert
        /// </param>
        /// <returns>
        /// An EF <seealso cref="AuthorizationRequest"/> <seealso cref="EntityObject"/>
        /// </returns>
        public static AuthorizationRequest ToEfAuthorizationRequest(this InsuranceModels.AuthorizationRequest request)
        {
            return new AuthorizationRequest
                {
                    AccountId = request.AccountId,
                    AuthorizationRequestId = request.Id,
                    CurrentVersion = request.CurrentVersion,
                    Description = request.Description,
                    InsurerId = request.InsurerId
                };
        }

        /// <summary>
        /// Converts an <seealso cref="InsuranceModels.AuthorizationNote"/> to an EF 
        /// <seealso cref="AuthorizationNote"/> entity
        /// </summary>
        /// <param name="note">
        /// The <seealso cref="InsuranceModels.AuthorizationNote"/> to convert
        /// </param>
        /// <returns>
        /// An EF <seealso cref="AuthorizationNote"/> <seealso cref="EntityObject"/>
        /// </returns>
        public static AuthorizationNote ToEfAuthorizationNote(this InsuranceModels.AuthorizationNote note)
        {
            return new AuthorizationNote
                {
                    AuthorizationNoteId = note.Id,
                    AuthorizationRequestId = note.AuthorizationRequestId,
                    Created = note.Created,
                    CurrentVersion = note.CurrentVersion,
                    Note = note.Note
                };
        }

        /// <summary>
        /// Converts an <seealso cref="InsuranceModels.AuthorizationFollowUp"/> to an EF 
        /// <seealso cref="AuthorizationFollowUp"/> entity
        /// </summary>
        /// <param name="followUp">
        /// The <seealso cref="InsuranceModels.AuthorizationFollowUp"/> to convert
        /// </param>
        /// <returns>
        /// An EF <seealso cref="AuthorizationFollowUp"/> <seealso cref="EntityObject"/>
        /// </returns>
        public static AuthorizationFollowUp ToEfAuthorizationFollowUp(this InsuranceModels.AuthorizationFollowUp followUp)
        {
            return new AuthorizationFollowUp
                {
                    AccountId = followUp.AccountId,
                    AppointmentDateTime = followUp.AppointmentDateTimeUtc,
                    AuthorizationFollowUpId = followUp.Id,
                    AuthorizationRequestId = followUp.AuthorizationRequestId,
                    CurrentVersion = followUp.CurrentVersion,
                    Description = followUp.Description
                };
        }

        /// <summary>
        /// Converts an <seealso cref="MedicalModels.Facility"/> to an EF 
        /// <seealso cref="Facility"/> entity
        /// </summary>
        /// <param name="facility">
        /// The <seealso cref="MedicalModels.Facility"/> to convert
        /// </param>
        /// <returns>
        /// An EF <seealso cref="Facility"/> <seealso cref="EntityObject"/>
        /// </returns>
        public static Facility ToEfFacility(this MedicalModels.Facility facility)
        {
            return new Facility
                {
                    AccountId = facility.AccountId,
                    CurrentVersion = facility.CurrentVersion,
                    FacilityId = facility.Id,
                    Name = facility.Name
                };
        }

        /// <summary>
        /// Converts an <seealso cref="MedicalModels.Provider"/> to an EF 
        /// <seealso cref="Provider"/> entity
        /// </summary>
        /// <param name="provider">
        /// The <seealso cref="MedicalModels.Provider"/> to convert
        /// </param>
        /// <returns>
        /// An EF <seealso cref="Provider"/> <seealso cref="EntityObject"/>
        /// </returns>
        public static Provider ToEfProvider(this MedicalModels.Provider provider)
        {
            return new Provider
                {
                    CurrentVersion = provider.CurrentVersion,
                    FacilityId = provider.FacilityId,
                    Name = provider.Name,
                    ProviderId = provider.Id
                };
        }

        /// <summary>
        /// Converts an <seealso cref="MedicalModels.MedicalAppointment"/> to an EF 
        /// <seealso cref="MedicalAppointment"/> entity
        /// </summary>
        /// <param name="appointment">
        /// The <seealso cref="MedicalModels.MedicalAppointment"/> to convert
        /// </param>
        /// <returns>
        /// An EF <seealso cref="MedicalAppointment"/> <seealso cref="EntityObject"/>
        /// </returns>
        public static MedicalAppointment ToEfAppointment(this MedicalModels.MedicalAppointment appointment)
        {
            return new MedicalAppointment
                {
                    AccountId = appointment.AccountId,
                    AppointmentDateTime = appointment.AppointmentDateTimeUtc,
                    CurrentVersion = appointment.CurrentVersion,
                    Description = appointment.Description,
                    Length = appointment.Length,
                    MedicalAppointmentId = appointment.Id,
                    ProviderId = appointment.ProviderId,
                    ScheduleUnitId =
                        appointment.AppointmentLengthUnits.HasValue ? (int)appointment.AppointmentLengthUnits.Value : 0
                };
        }

        /// <summary>
        /// Converts an <seealso cref="PrescriptionModels.Medication"/> to an EF 
        /// <seealso cref="Medication"/> entity
        /// </summary>
        /// <param name="medication">
        /// The <seealso cref="PrescriptionModels.Medication"/> to convert
        /// </param>
        /// <returns>
        /// An EF <seealso cref="Medication"/> <seealso cref="EntityObject"/>
        /// </returns>
        public static Medication ToEfMedication(this PrescriptionModels.Medication medication)
        {
            return new Medication
            {
                AccountId = medication.AccountId,
                CurrentVersion = medication.CurrentVersion,
                DosageUnitId = medication.DosageUnits.HasValue ? (int)medication.DosageUnits.Value : 0,
                MedicationId = medication.Id,
                Name = medication.Name,
                Quantity = medication.Quantity
            };
        }

        /// <summary>
        /// Converts an <seealso cref="PrescriptionModels.PrescriptionPickup"/> to an EF 
        /// <seealso cref="PrescriptionPickup"/> entity
        /// </summary>
        /// <param name="pickup">
        /// The <seealso cref="PrescriptionModels.PrescriptionPickup"/> to convert
        /// </param>
        /// <returns>
        /// An EF <seealso cref="PrescriptionPickup"/> <seealso cref="EntityObject"/>
        /// </returns>
        public static PrescriptionPickup ToEfPrescriptionPickup(this PrescriptionModels.PrescriptionPickup pickup)
        {
            return new PrescriptionPickup
            {
                AccountId = pickup.AccountId,
                AppointmentDateTime = pickup.AppointmentDateTimeUtc,
                CurrentVersion = pickup.CurrentVersion,
                MedicationId = pickup.MedicationId,
                PrescriptionPickupId = pickup.Id
            };
        }
    }
}
