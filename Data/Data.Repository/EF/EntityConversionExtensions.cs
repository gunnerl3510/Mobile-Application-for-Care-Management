
namespace Data.Repository.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AccountModels = Infrastructure.Model.Account;
    using InsuranceModels = Infrastructure.Model.Insurance;

    /// <summary>
    /// Maintains the EF entity to Model conversion mappings
    /// </summary>
    internal static class EntityConversionExtensions
    {
        #region private static members

        /// <summary>
        /// The single instance of the Kbm2 EF data context
        /// </summary>
        // ReSharper disable StaticFieldInGenericType
        private static readonly CareManagementContainer Container = new CareManagementContainer();
        // ReSharper restore StaticFieldInGenericType

        #endregion

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
                UserId = (Guid)account.UserId
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
        public static InsuranceModels.AuthorizationRequest ToModelAuthorizationRequest(this AuthorizationRequest request)
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
        public static InsuranceModels.AuthorizationFollowUp ToModelAuthorizationFollowUp(this AuthorizationFollowUp followUp)
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
    }
}
