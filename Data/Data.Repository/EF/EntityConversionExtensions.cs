
namespace Data.Repository.EF
{
    using System;

    using AccountModels = Infrastructure.Model.Account;
    using InsuranceModels = Infrastructure.Model.Insurance;
    using MedicalModels = Infrastructure.Model.Medical;
    using PrescriptionModels = Infrastructure.Model.Prescription;
    using SchedulingModels = Infrastructure.Model.Scheduling;

    /// <summary>
    /// Maintains the EF entity to Model conversion mappings
    /// </summary>
    internal static class EntityConversionExtensions
    {
        /// <summary>
        /// Converts an EF <seealso cref="Account"/> to a 
        /// <seealso cref="AccountModels.Account"/>
        /// </summary>
        /// <param name="account">
        /// The <seealso cref="Account"/> to convert
        /// </param>
        /// <returns>A <seealso cref="AccountModels.Account"/></returns>
        public static AccountModels.Account ToModelAccount(this Account account)
        {
            return new AccountModels.Account
                {
                    Id = account.AccountId,
                    CurrentVersion = account.CurrentVersion,
                    EmailAddress = account.EmailAddress,
                    Name = account.Name,
                    // ReSharper disable PossibleInvalidOperationException
                    UserId = account.UserId
                    // ReSharper restore PossibleInvalidOperationException
                };
        }

        /// <summary>
        /// Converts an EF <seealso cref="Insurer"/> to a 
        /// <seealso cref="InsuranceModels.Insurer"/>
        /// </summary>
        /// <param name="insurer">
        /// The <seealso cref="Insurer"/> to convert
        /// </param>
        /// <returns>A <seealso cref="InsuranceModels.Insurer"/></returns>
        public static InsuranceModels.Insurer ToModelInsurer(this Insurer insurer)
        {
            return new InsuranceModels.Insurer
                {
                    AccountId = insurer.AccountId,
                    CurrentVersion = insurer.CurrentVersion,
                    Id = insurer.InsurerId,
                    Name = insurer.Name
                };
        }

        /// <summary>
        /// Converts an EF <seealso cref="AuthorizationRequest"/> to a 
        /// <seealso cref="InsuranceModels.AuthorizationRequest"/>
        /// </summary>
        /// <param name="request">
        /// The <seealso cref="AuthorizationRequest"/> to convert
        /// </param>
        /// <returns>A <seealso cref="InsuranceModels.AuthorizationRequest"/></returns>
        public static InsuranceModels.AuthorizationRequest ToModelAuthorizationRequest(
            this AuthorizationRequest request)
        {
            return new InsuranceModels.AuthorizationRequest
                {
                    AccountId = request.AccountId,
                    CurrentVersion = request.CurrentVersion,
                    Description = request.Description,
                    Id = request.AuthorizationRequestId,
                    InsurerId = request.InsurerId
                };
        }

        /// <summary>
        /// Converts an EF <seealso cref="AuthorizationNote"/> to a 
        /// <seealso cref="InsuranceModels.AuthorizationNote"/>
        /// </summary>
        /// <param name="note">
        /// The <seealso cref="AuthorizationNote"/> to convert
        /// </param>
        /// <returns>A <seealso cref="InsuranceModels.AuthorizationNote"/></returns>
        public static InsuranceModels.AuthorizationNote ToModelAuthorizationNote(this AuthorizationNote note)
        {
            return new InsuranceModels.AuthorizationNote
                {
                    AuthorizationRequestId = note.AuthorizationRequestId,
                    CurrentVersion = note.CurrentVersion,
                    Created = note.Created,
                    Id = note.AuthorizationNoteId,
                    Note = note.Note
                };
        }

        /// <summary>
        /// Converts an EF <seealso cref="AuthorizationFollowUp"/> to a 
        /// <seealso cref="InsuranceModels.AuthorizationFollowUp"/>
        /// </summary>
        /// <param name="followUp">
        /// The <seealso cref="AuthorizationFollowUp"/> to convert
        /// </param>
        /// <returns>A <seealso cref="InsuranceModels.AuthorizationNote"/></returns>
        public static InsuranceModels.AuthorizationFollowUp ToModelAuthorizationFollowUp(
            this AuthorizationFollowUp followUp)
        {
            return new InsuranceModels.AuthorizationFollowUp
                {
                    AccountId = followUp.AccountId,
                    AppointmentDateTimeUtc = followUp.AppointmentDateTime,
                    AuthorizationRequestId = followUp.AuthorizationRequestId,
                    CurrentVersion = followUp.CurrentVersion,
                    Description = followUp.Description,
                    Id = followUp.AuthorizationFollowUpId
                };
        }

        /// <summary>
        /// Converts an EF <seealso cref="Facility"/> to a 
        /// <seealso cref="MedicalModels.Facility"/>
        /// </summary>
        /// <param name="facility">
        /// The <seealso cref="Facility"/> to convert
        /// </param>
        /// <returns>A <seealso cref="MedicalModels.Facility"/></returns>
        public static MedicalModels.Facility ToModelFacility(this Facility facility)
        {
            return new MedicalModels.Facility
                {
                    AccountId = facility.AccountId,
                    CurrentVersion = facility.CurrentVersion,
                    Id = facility.FacilityId,
                    Name = facility.Name
                };
        }

        /// <summary>
        /// Converts an EF <seealso cref="Provider"/> to a 
        /// <seealso cref="MedicalModels.Provider"/>
        /// </summary>
        /// <param name="provider">
        /// The <seealso cref="Provider"/> to convert
        /// </param>
        /// <returns>A <seealso cref="MedicalModels.Provider"/></returns>
        public static MedicalModels.Provider ToModelProvider(this Provider provider)
        {
            return new MedicalModels.Provider
                {
                    CurrentVersion = provider.CurrentVersion,
                    FacilityId = provider.FacilityId,
                    Id = provider.ProviderId,
                    Name = provider.Name
                };
        }

        /// <summary>
        /// Converts an EF <seealso cref="MedicalAppointment"/> to a 
        /// <seealso cref="MedicalModels.MedicalAppointment"/>
        /// </summary>
        /// <param name="appointment">
        /// The <seealso cref="MedicalAppointment"/> to convert
        /// </param>
        /// <returns>A <seealso cref="MedicalModels.MedicalAppointment"/></returns>
        public static MedicalModels.MedicalAppointment ToModelMedicalAppointment(this MedicalAppointment appointment)
        {
            return new MedicalModels.MedicalAppointment
                {
                    AccountId = appointment.AccountId,
                    AppointmentDateTimeUtc = appointment.AppointmentDateTime,
                    AppointmentLengthUnitsValue = appointment.ScheduleUnitId,
                    CurrentVersion = appointment.CurrentVersion,
                    Description = appointment.Description,
                    Id = appointment.MedicalAppointmentId,
                    Length = appointment.Length,
                    ProviderId = appointment.ProviderId
                };
        }

        /// <summary>
        /// Converts an EF <seealso cref="Medication"/> to a 
        /// <seealso cref="PrescriptionModels.Medication"/>
        /// </summary>
        /// <param name="medicine">
        /// The <seealso cref="Medication"/> to convert
        /// </param>
        /// <returns>A <seealso cref="PrescriptionModels.Medication"/></returns>
        public static PrescriptionModels.Medication ToModelMedication(this Medication medicine)
        {
            return new PrescriptionModels.Medication
                {
                    AccountId = medicine.AccountId,
                    CurrentVersion = medicine.CurrentVersion,
                    DosageUnitsValue = medicine.DosageUnitId,
                    Id = medicine.MedicationId,
                    Name = medicine.Name,
                    Quantity = medicine.Quantity
                };
        }

        /// <summary>
        /// Converts an EF <seealso cref="PrescriptionPickup"/> to a 
        /// <seealso cref="PrescriptionModels.PrescriptionPickup"/>
        /// </summary>
        /// <param name="pickup">
        /// The <seealso cref="PrescriptionPickup"/> to convert
        /// </param>
        /// <returns>A <seealso cref="PrescriptionModels.PrescriptionPickup"/></returns>
        public static PrescriptionModels.PrescriptionPickup ToModelPrescriptionPickup(this PrescriptionPickup pickup)
        {
            return new PrescriptionModels.PrescriptionPickup
            {
                AccountId = pickup.AccountId,
                AppointmentDateTimeUtc = pickup.AppointmentDateTime,
                CurrentVersion = pickup.CurrentVersion,
                Id = pickup.PrescriptionPickupId,
                MedicationId = pickup.MedicationId
            };
        }
    }
}
