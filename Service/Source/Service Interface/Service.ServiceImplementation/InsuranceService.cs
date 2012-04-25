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
    using System.Security.Permissions;
    using System.ServiceModel;

    using BusinessLogic.Insurance;

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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void AddInsurer(Service.MessageContracts.InsurerMessage request)
        {
            insuranceManager.CreateInsurer(request.Insurer, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes an insurer through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the insurer to delete
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void DeleteInsurer(Service.MessageContracts.InsurerMessage request)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.InsurerMessage GetInsurer(Service.MessageContracts.InsurerIdMessage request)
        {
            return new Service.MessageContracts.InsurerMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void UpdateInsurer(Service.MessageContracts.InsurerMessage request)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.InsurersMessage GetInsurersByAccount(Service.MessageContracts.AccountIdMessage request)
        {
            return new Service.MessageContracts.InsurersMessage
            {
                Insurers = 
                    insuranceManager
                        .GetInsurersByAccount(request.AccountId, ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Adds a new follow up to an authorization through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the follow up to add
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void AddAuthorizationFollowUp(Service.MessageContracts.AuthorizationFollowUpMessage request)
        {
            followUpManager.CreateAuthorizationFollowUp(request.AuthorizationFollowUp, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes a follow up to an authorization through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the follow up to delete
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void DeleteAuthorizationFollowUp(Service.MessageContracts.AuthorizationFollowUpMessage request)
        {
            followUpManager.DeleteAuthorizationFollowUp(request.AuthorizationFollowUp, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates a follow up to an authorization through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the follow up to delete
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void UpdateAuthorizationFollowUp(Service.MessageContracts.AuthorizationFollowUpMessage request)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.AuthorizationFollowUpsMessage GetAuthorizationFollowUpsByAccount(Service.MessageContracts.AccountIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationFollowUpsMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.AuthorizationFollowUpsMessage GetAuthorizationFollowUpsByAuthorizationRequest(Service.MessageContracts.AuthorizationRequestIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationFollowUpsMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.AuthorizationFollowUpMessage GetAuthorizationFollowUp(Service.MessageContracts.AuthorizationFollowUpIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationFollowUpMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void AddAuthorizationRequest(Service.MessageContracts.AuthorizationRequestMessage request)
        {
            requestManager.CreateAuthorizationRequest(request.AuthorizationRequest, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes an authorization request through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the request to delete
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void DeleteAuthorizationRequest(Service.MessageContracts.AuthorizationRequestMessage request)
        {
            requestManager.DeleteAuthorizationRequest(request.AuthorizationRequest, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates an authorization request through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the request to update
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void UpdateAuthorizationRequest(Service.MessageContracts.AuthorizationRequestMessage request)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.AuthorizationRequestMessage GetAuthorizationRequest(Service.MessageContracts.AuthorizationRequestIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationRequestMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.AuthorizationRequestsMessage GetAuthorizationRequestsByAccount(Service.MessageContracts.AccountIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationRequestsMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.AuthorizationRequestsMessage GetAuthorizationRequestsByInsurer(Service.MessageContracts.InsurerIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationRequestsMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void AddAuthorizationNote(Service.MessageContracts.AuthorizationNoteMessage request)
        {
            noteManager.CreateAuthorizationNote(request.AuthorizationNote, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes an authorization note through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the note to delete
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void DeleteAuthorizationNote(Service.MessageContracts.AuthorizationNoteMessage request)
        {
            noteManager.DeleteAuthorizationNote(request.AuthorizationNote, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates an authorization note through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the note to update
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void UpdateAuthorizationNote(Service.MessageContracts.AuthorizationNoteMessage request)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.AuthorizationNoteMessage GetAuthorizationNote(Service.MessageContracts.AuthorizationNoteIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationNoteMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.AuthorizationNotesMessage GetAuthorizationNotesByAccount(Service.MessageContracts.AccountIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationNotesMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.AuthorizationNotesMessage GetAuthorizationNotesByAuthorizationRequest(Service.MessageContracts.AuthorizationRequestIdMessage request)
        {
            return new Service.MessageContracts.AuthorizationNotesMessage
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
