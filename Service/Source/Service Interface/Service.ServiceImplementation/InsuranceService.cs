// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsuranceService.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates the service logic for working with objects in the Insurance domain
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Service.ServiceImplementation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

    using BusinessLogic.Insurance;

    using Infrastructure.Model.Insurance;
    using Infrastructure.Security;

    using Ninject;

    /// <summary>
    /// Encapsulates the service logic for working with objects in the Insurance domain
    /// </summary>
    public partial class InsuranceService
    {
        /// <summary>
        /// The business logic object for manipulating insurer objects
        /// </summary>
        private readonly Insurers insuranceManager;

        /// <summary>
        /// The business logic object for manipulating account objects
        /// </summary>
        private readonly AuthorizationRequests requestManager;

        /// <summary>
        /// The business logic object for manipulating account objects
        /// </summary>
        private readonly AuthorizationFollowUps followUpManager;

        /// <summary>
        /// The business logic object for manipulating account objects
        /// </summary>
        private readonly AuthorizationNotes noteManager;

        /// <summary>
        /// The dependecy injection kernel
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceService" />
        /// class
        /// </summary>
        /// <param name="kernel">
        /// The dependency injection object
        /// </param>
        public InsuranceService(IKernel kernel)
        {
            this.kernel = kernel;
            insuranceManager = this.kernel.Get<Insurers>();
            requestManager = this.kernel.Get<AuthorizationRequests>();
            followUpManager = this.kernel.Get<AuthorizationFollowUps>();
            noteManager = this.kernel.Get<AuthorizationNotes>();
        }

        #region InsuranceContract Members

        /// <summary>
        /// Adds a new insurer through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the insurer to add
        /// </param>
        public override void AddInsurer(MessageContracts.InsurerRequestMessage request)
        {
            insuranceManager.CreateInsurer(request.Insurer, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes an insurer through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the insurer to delete
        /// </param>
        public override void DeleteInsurer(MessageContracts.InsurerRequestMessage request)
        {
            insuranceManager.DeleteInsurer(request.Insurer, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Retrieves an insurer through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the insurer to retrieve
        /// </param>
        /// <returns>
        /// A message that contains the <seealso cref="Insurer"/> that was retrieved
        /// </returns>
        public override MessageContracts.InsurerMessage GetInsurer(MessageContracts.InsurerIdRequestMessage request)
        {
            return new MessageContracts.InsurerMessage
            {
                Insurer = insuranceManager.GetInsurerById(request.InsurerId, ServiceSecurityContext.Current.PrimaryIdentity)
            };
        }

        /// <summary>
        /// Updates an insurer through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the insurer to update
        /// </param>
        public override void UpdateInsurer(MessageContracts.InsurerRequestMessage request)
        {
            insuranceManager.UpdateInsurer(request.Insurer, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="Insurer"/> through the business logic objects
        /// for the account specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the account to retrieve the insurers for
        /// </param>
        /// <returns>
        /// A message that contains a <seealso cref="List{T}"/> of <seealso cref="Insurer"/> that was retrieved
        /// </returns>
        public override MessageContracts.InsurersMessage GetInsurersByAccount(MessageContracts.AccountIdInsuranceRequestMessage request)
        {
            return new MessageContracts.InsurersMessage
            {
                Insurers = 
                    insuranceManager
                        .GetInsurers(ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Adds a new follow up to an authorization through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the follow up to add
        /// </param>
        public override void AddAuthorizationFollowUp(MessageContracts.AuthorizationFollowUpRequestMessage request)
        {
            followUpManager.CreateAuthorizationFollowUp(request.AuthorizationFollowUp, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes a follow up to an authorization through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the follow up to delete
        /// </param>
        public override void DeleteAuthorizationFollowUp(MessageContracts.AuthorizationFollowUpRequestMessage request)
        {
            followUpManager.DeleteAuthorizationFollowUp(request.AuthorizationFollowUp, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates a follow up to an authorization through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the follow up to delete
        /// </param>
        public override void UpdateAuthorizationFollowUp(MessageContracts.AuthorizationFollowUpRequestMessage request)
        {
            followUpManager.UpdateAuthorizationFollowUp(request.AuthorizationFollowUp, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="AuthorizationFollowUp"/>
        /// through the business logic objects for the account specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the account to retrieve the follow ups for
        /// </param>
        /// <returns>
        /// A message that contains the <seealso cref="AuthorizationFollowUp"/> that was retrieved
        /// </returns>
        public override MessageContracts.AuthorizationFollowUpsMessage GetAuthorizationFollowUpsByAccount(MessageContracts.AccountIdInsuranceRequestMessage request)
        {
            return new MessageContracts.AuthorizationFollowUpsMessage
            {
                AuthorizationFollowUps =
                    followUpManager
                        .GetAuthorizationFollowUpsByAccount(
                            request.AccountId, 
                            ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="AuthorizationFollowUp"/>
        /// through the business logic objects for the authorization request specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the authorization request to retrieve the follow ups for
        /// </param>
        /// <returns>
        /// A message that contains the <seealso cref="List{T}"/> of <seealso cref="Insurer"/> that was retrieved
        /// </returns>
        public override MessageContracts.AuthorizationFollowUpsMessage GetAuthorizationFollowUpsByAuthorizationRequest(MessageContracts.AuthorizationRequestIdMessage request)
        {
            return new MessageContracts.AuthorizationFollowUpsMessage
            {
                AuthorizationFollowUps =
                    followUpManager
                        .GetAuthorizationFollowUpsByAuthorizationRequest(
                            request.AuthorizationRequestId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Retrieves an authorization follow up through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the follow up to retrieve
        /// </param>
        /// <returns>
        /// A message that contains the <seealso cref="AuthorizationFollowUp"/> that was retrieved
        /// </returns>
        public override MessageContracts.AuthorizationFollowUpMessage GetAuthorizationFollowUp(MessageContracts.AuthorizationFollowUpIdRequestMessage request)
        {
            return new MessageContracts.AuthorizationFollowUpMessage
            {
                AuthorizationFollowUp = 
                    followUpManager
                        .GetAuthorizationFollowUpById(
                            request.AuthorizationFollowUpId, 
                            ServiceSecurityContext.Current.PrimaryIdentity)
            };
        }

        /// <summary>
        /// Adds a new authorization request through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the request to add
        /// </param>
        public override void AddAuthorizationRequest(MessageContracts.AuthorizationRequestRequestMessage request)
        {
            requestManager.CreateAuthorizationRequest(request.AuthorizationRequest, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes an authorization request through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the request to delete
        /// </param>
        public override void DeleteAuthorizationRequest(MessageContracts.AuthorizationRequestRequestMessage request)
        {
            requestManager.DeleteAuthorizationRequest(request.AuthorizationRequest, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates an authorization request through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the request to update
        /// </param>
        public override void UpdateAuthorizationRequest(MessageContracts.AuthorizationRequestRequestMessage request)
        {
            requestManager.UpdateAuthorizationRequest(request.AuthorizationRequest, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Retrieves an authorization request through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the request to retrieve
        /// </param>
        /// <returns>
        /// A message that contains the <seealso cref="AuthorizationRequest"/> that was retrieved
        /// </returns>
        public override MessageContracts.AuthorizationRequestMessage GetAuthorizationRequest(MessageContracts.AuthorizationRequestIdMessage request)
        {
            return new MessageContracts.AuthorizationRequestMessage
            {
                AuthorizationRequest =
                    requestManager
                        .GetAuthorizationRequestById(
                            request.AuthorizationRequestId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="AuthorizationRequest"/>
        /// through the business logic objects for the account specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the account to retrieve the requests for
        /// </param>
        /// <returns>
        /// A message that contains the <seealso cref="List{T}"/> of <seealso cref="AuthorizationRequest"/> that was retrieved
        /// </returns>
        public override MessageContracts.AuthorizationRequestsMessage GetAuthorizationRequestsByAccount(MessageContracts.AccountIdInsuranceRequestMessage request)
        {
            return new MessageContracts.AuthorizationRequestsMessage
            {
                AuthorizationRequests =
                    requestManager
                        .GetAuthorizationRequestsByAccount(
                            request.AccountId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="AuthorizationRequest"/>
        /// through the business logic objects for the insurer specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the insurer to retrieve the requests for
        /// </param>
        /// <returns>
        /// A message that contains the <seealso cref="List{T}"/> of <seealso cref="AuthorizationRequest"/> that was retrieved
        /// </returns>
        public override MessageContracts.AuthorizationRequestsMessage GetAuthorizationRequestsByInsurer(MessageContracts.InsurerIdRequestMessage request)
        {
            return new MessageContracts.AuthorizationRequestsMessage
            {
                AuthorizationRequests =
                    requestManager
                        .GetAuthorizationRequestsByInsurer(
                            request.InsurerId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Adds a new authorization note through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the note to add
        /// </param>
        public override void AddAuthorizationNote(MessageContracts.AuthorizationNoteRequestMessage request)
        {
            noteManager.CreateAuthorizationNote(request.AuthorizationNote, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes an authorization note through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the note to delete
        /// </param>
        public override void DeleteAuthorizationNote(MessageContracts.AuthorizationNoteRequestMessage request)
        {
            noteManager.DeleteAuthorizationNote(request.AuthorizationNote, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates an authorization note through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the note to update
        /// </param>
        public override void UpdateAuthorizationNote(MessageContracts.AuthorizationNoteRequestMessage request)
        {
            noteManager.UpdateAuthorizationNote(request.AuthorizationNote, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Retrieves an authorization note through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the note to retrieve
        /// </param>
        /// <returns>
        /// A message that contains the <seealso cref="AuthorizationNote"/> that was retrieved
        /// </returns>
        public override MessageContracts.AuthorizationNoteMessage GetAuthorizationNote(MessageContracts.AuthorizationNoteIdRequestMessage request)
        {
            return new MessageContracts.AuthorizationNoteMessage
            {
                AuthorizationNote =
                    noteManager
                        .GetAuthorizationNoteById(
                            request.AuthorizationNoteId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="AuthorizationNote"/>
        /// through the business logic objects for the account specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the account to retrieve the notes for
        /// </param>
        /// <returns>
        /// A message that contains the <seealso cref="List{T}"/> of <seealso cref="AuthorizationNote"/> that was retrieved
        /// </returns>
        public override MessageContracts.AuthorizationNotesMessage GetAuthorizationNotesByAccount(MessageContracts.AccountIdInsuranceRequestMessage request)
        {
            return new MessageContracts.AuthorizationNotesMessage
            {
                AuthorizationNotes =
                    noteManager
                        .GetAuthorizationNotesByAccount(
                            request.AccountId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="AuthorizationNote"/>
        /// through the business logic objects for the authorization request specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the request to retrieve the notes for
        /// </param>
        /// <returns>
        /// A message that contains the <seealso cref="List{T}"/> of <seealso cref="AuthorizationNote"/> that was retrieved
        /// </returns>
        public override MessageContracts.AuthorizationNotesMessage GetAuthorizationNotesByAuthorizationRequest(MessageContracts.AuthorizationRequestIdMessage request)
        {
            return new MessageContracts.AuthorizationNotesMessage
            {
                AuthorizationNotes =
                    noteManager
                        .GetAuthorizationNotesByAuthorizationRequest(
                            request.AuthorizationRequestId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        #endregion
    }
}
