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
    using System.Linq;

    using BusinessLogic.Insurance;

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
        private Insurers insuranceManager;

        /// <summary>
        /// The business logic object for manipulating account objects
        /// </summary>
        private AuthorizationRequests requestManager;

        /// <summary>
        /// The business logic object for manipulating account objects
        /// </summary>
        private AuthorizationFollowUps followUpManager;

        /// <summary>
        /// The business logic object for manipulating account objects
        /// </summary>
        private AuthorizationNotes noteManager;

        /// <summary>
        /// The dependecy injection kernel
        /// </summary>
        private IKernel kernel;

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

        /// <summary>
        /// Prevents a default instance of the <see cref="InsuranceService" />
        /// class from being created.
        /// </summary>
        private InsuranceService()
        {
        }

        #region InsuranceContract Members

        /// <summary>
        /// Adds a new insurer through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the insurer to add
        /// </param>
        public override void AddInsurer(Service.MessageContracts.InsurerRequestMessage request)
        {
            insuranceManager.CreateInsurer(request.Insurer, request.User.GetIdentity());
        }

        /// <summary>
        /// Deletes an insurer through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the insurer to delete
        /// </param>
        public override void DeleteInsurer(Service.MessageContracts.InsurerRequestMessage request)
        {
            insuranceManager.DeleteInsurer(request.Insurer, request.User.GetIdentity());
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
        public override Service.MessageContracts.InsurerMessage GetInsurer(Service.MessageContracts.InsurerIdRequestMessage request)
        {
            return new Service.MessageContracts.InsurerMessage
            {
                Insurer = insuranceManager.GetInsurerById(request.InsurerId, request.User.GetIdentity())
            };
        }

        /// <summary>
        /// Updates an insurer through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the insurer to update
        /// </param>
        public override void UpdateInsurer(Service.MessageContracts.InsurerRequestMessage request)
        {
            insuranceManager.UpdateInsurer(request.Insurer, request.User.GetIdentity());
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
        public override Service.MessageContracts.InsurersMessage GetInsurersByAccount(Service.MessageContracts.AccountIdRequestMessage request)
        {
            return new Service.MessageContracts.InsurersMessage
            {
                Insurers = 
                    insuranceManager
                        .GetInsurersByAccount(request.AccountId, request.User.GetIdentity())
                        .ToList()
            };
        }

        /// <summary>
        /// Adds a new follow up to an authorization through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the follow up to add
        /// </param>
        public override void AddAuthorizationFollowUp(Service.MessageContracts.AuthorizationFollowUpRequestMessage request)
        {
            followUpManager.CreateAuthorizationFollowUp(request.AuthorizationFollowUp, request.User.GetIdentity());
        }

        /// <summary>
        /// Deletes a follow up to an authorization through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the follow up to delete
        /// </param>
        public override void DeleteAuthorizationFollowUp(Service.MessageContracts.AuthorizationFollowUpRequestMessage request)
        {
            followUpManager.DeleteAuthorizationFollowUp(request.AuthorizationFollowUp, request.User.GetIdentity());
        }

        /// <summary>
        /// Updates a follow up to an authorization through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the follow up to delete
        /// </param>
        public override void UpdateAuthorizationFollowUp(Service.MessageContracts.AuthorizationFollowUpRequestMessage request)
        {
            followUpManager.UpdateAuthorizationFollowUp(request.AuthorizationFollowUp, request.User.GetIdentity());
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
        public override Service.MessageContracts.AuthorizationFollowUpsMessage GetAuthorizationFollowUpsByAccount(Service.MessageContracts.AccountIdRequestMessage request)
        {
            return new Service.MessageContracts.AuthorizationFollowUpsMessage
            {
                AuthorizationFollowUps =
                    followUpManager
                        .GetAuthorizationFollowUpsByAccount(
                            request.AccountId, 
                            request.User.GetIdentity())
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
        public override Service.MessageContracts.AuthorizationFollowUpsMessage GetAuthorizationFollowUpsByAuthorizationRequest(Service.MessageContracts.AuthorizationRequestIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationFollowUpsMessage
            {
                AuthorizationFollowUps =
                    followUpManager
                        .GetAuthorizationFollowUpsByAuthorizationRequest(
                            request.AuthorizationRequestId,
                            request.User.GetIdentity())
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
        public override Service.MessageContracts.AuthorizationFollowUpMessage GetAuthorizationFollowUp(Service.MessageContracts.AuthorizationFollowUpIdRequestMessage request)
        {
            return new Service.MessageContracts.AuthorizationFollowUpMessage
            {
                AuthorizationFollowUp = 
                    followUpManager
                        .GetAuthorizationFollowUpById(
                            request.AuthorizationFollowUpId, 
                            request.User.GetIdentity())
            };
        }

        /// <summary>
        /// Adds a new authorization request through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the request to add
        /// </param>
        public override void AddAuthorizationRequest(Service.MessageContracts.AuthorizationRequestRequestMessage request)
        {
            requestManager.CreateAuthorizationRequest(request.AuthorizationRequest, request.User.GetIdentity());
        }

        /// <summary>
        /// Deletes an authorization request through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the request to delete
        /// </param>
        public override void DeleteAuthorizationRequest(Service.MessageContracts.AuthorizationRequestRequestMessage request)
        {
            requestManager.DeleteAuthorizationRequest(request.AuthorizationRequest, request.User.GetIdentity());
        }

        /// <summary>
        /// Updates an authorization request through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the request to update
        /// </param>
        public override void UpdateAuthorizationRequest(Service.MessageContracts.AuthorizationRequestRequestMessage request)
        {
            requestManager.UpdateAuthorizationRequest(request.AuthorizationRequest, request.User.GetIdentity());
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
        public override Service.MessageContracts.AuthorizationRequestMessage GetAuthorizationRequest(Service.MessageContracts.AuthorizationRequestIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationRequestMessage
            {
                AuthorizationRequest =
                    requestManager
                        .GetAuthorizationRequestById(
                            request.AuthorizationRequestId,
                            request.User.GetIdentity())
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
        public override Service.MessageContracts.AuthorizationRequestsMessage GetAuthorizationRequestsByAccount(Service.MessageContracts.AccountIdRequestMessage request)
        {
            return new Service.MessageContracts.AuthorizationRequestsMessage
            {
                AuthorizationRequests =
                    requestManager
                        .GetAuthorizationRequestsByAccount(
                            request.AccountId,
                            request.User.GetIdentity())
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
        public override Service.MessageContracts.AuthorizationRequestsMessage GetAuthorizationRequestsByInsurer(Service.MessageContracts.InsurerIdRequestMessage request)
        {
            return new Service.MessageContracts.AuthorizationRequestsMessage
            {
                AuthorizationRequests =
                    requestManager
                        .GetAuthorizationRequestsByInsurer(
                            request.InsurerId,
                            request.User.GetIdentity())
                        .ToList()
            };
        }

        /// <summary>
        /// Adds a new authorization note through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the note to add
        /// </param>
        public override void AddAuthorizationNote(Service.MessageContracts.AuthorizationNoteRequestMessage request)
        {
            noteManager.CreateAuthorizationNote(request.AuthorizationNote, request.User.GetIdentity());
        }

        /// <summary>
        /// Deletes an authorization note through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the note to delete
        /// </param>
        public override void DeleteAuthorizationNote(Service.MessageContracts.AuthorizationNoteRequestMessage request)
        {
            noteManager.DeleteAuthorizationNote(request.AuthorizationNote, request.User.GetIdentity());
        }

        /// <summary>
        /// Updates an authorization note through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the note to update
        /// </param>
        public override void UpdateAuthorizationNote(Service.MessageContracts.AuthorizationNoteRequestMessage request)
        {
            noteManager.UpdateAuthorizationNote(request.AuthorizationNote, request.User.GetIdentity());
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
        public override Service.MessageContracts.AuthorizationNoteMessage GetAuthorizationNote(Service.MessageContracts.AuthorizationNoteIdRequestMessage request)
        {
            return new Service.MessageContracts.AuthorizationNoteMessage
            {
                AuthorizationNote =
                    noteManager
                        .GetAuthorizationNoteById(
                            request.AuthorizationNoteId,
                            request.User.GetIdentity())
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
        public override Service.MessageContracts.AuthorizationNotesMessage GetAuthorizationNotesByAccount(Service.MessageContracts.AccountIdRequestMessage request)
        {
            return new Service.MessageContracts.AuthorizationNotesMessage
            {
                AuthorizationNotes =
                    noteManager
                        .GetAuthorizationNotesByAccount(
                            request.AccountId,
                            request.User.GetIdentity())
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
        public override Service.MessageContracts.AuthorizationNotesMessage GetAuthorizationNotesByAuthorizationRequest(Service.MessageContracts.AuthorizationRequestIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationNotesMessage
            {
                AuthorizationNotes =
                    noteManager
                        .GetAuthorizationNotesByAuthorizationRequest(
                            request.AuthorizationRequestId,
                            request.User.GetIdentity())
                        .ToList()
            };
        }

        #endregion
    }
}
