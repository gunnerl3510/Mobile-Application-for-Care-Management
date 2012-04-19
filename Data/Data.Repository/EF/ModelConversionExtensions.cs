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
    using System.Data.Objects.DataClasses;

    using AccountModels = Infrastructure.Model.Account;
    using InsuranceModels = Infrastructure.Model.Insurance;

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
            return new Account
            {
                AccountId = account.Id,
                CurrentVersion = account.CurrentVersion,
                EmailAddress = account.EmailAddress,
                Name = account.Name,
                UserId = account.UserId
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
        /// <param name="note">
        /// The <seealso cref="InsuranceModels.AuthorizationFollowUp"/> to convert
        /// </param>
        /// <returns>
        /// An EF <seealso cref="AuthorizationFollowUp"/> <seealso cref="EntityObject"/>
        /// </returns>
        public static AuthorizationFollowUp ToEfAuthorizationFollowUp(this InsuranceModels.AuthorizationFollowUp note)
        {
            return new AuthorizationFollowUp
            {
                AccountId = note.AccountId,
                AppointmentDateTime = note.AppointmentDateTimeUtc,
                AuthorizationFollowUpId = note.Id,
                AuthorizationRequestId = note.AuthorizationRequestId,
                CurrentVersion = note.CurrentVersion,
                Description = note.Description
            };
        }
    }
}
