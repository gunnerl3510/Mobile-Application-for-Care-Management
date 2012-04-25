// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountService.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates the service logic for working with objects in the Account domain
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Service.ServiceImplementation
{
    using System.Security.Permissions;
    using System.ServiceModel;

    using BusinessLogic.Account;

    using Ninject;

    /// <summary>
    /// Encapsulates the service logic for working with objects in the Account domain
    /// </summary>
    public partial class AccountService
    {
        /// <summary>
        /// The business logic object for manipulating account objects
        /// </summary>
        private Accounts accountsManager;

        /// <summary>
        /// The dependecy injection kernel
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService" />
        /// class
        /// </summary>
        /// <param name="kernel">
        /// The dependency injection object
        /// </param>
        public AccountService(IKernel kernel)
        {
            this.kernel = kernel;
            accountsManager = this.kernel.Get<Accounts>();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AccountService" />
        /// class from being created.
        /// </summary>
        private AccountService()
        {
        }
        
        #region AccountContract Members

        /// <summary>
        /// Adds an account through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the account to add and the password
        /// </param>
        public override void AddAccount(Service.MessageContracts.AddAccountMessage request)
        {
            accountsManager.AddAccount(request.Account, request.Password);
        }

        /// <summary>
        /// Deletes an account through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the account to delete
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void DeleteAccount(Service.MessageContracts.AccountMessage request)
        {
            accountsManager.DeleteAccount(request.Account, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates an account through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the account to update
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void UpdateAccount(Service.MessageContracts.AccountMessage request)
        {
            accountsManager.UpdateAccount(request.Account, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Gets an account through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the account id of the account to retrieve
        /// </param>
        /// <returns>
        /// An message containing the requested account
        /// </returns>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.AccountMessage GetAccount(Service.MessageContracts.AccountIdMessage request)
        {
            return new Service.MessageContracts.AccountMessage
            {
                Account = accountsManager.GetAccountById(request.AccountId, ServiceSecurityContext.Current.PrimaryIdentity)
            };
        }

        #endregion
    }
}
