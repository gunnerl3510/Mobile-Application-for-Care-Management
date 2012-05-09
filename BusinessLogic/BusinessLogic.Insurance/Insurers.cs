// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Insurers.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Business logic for working with Insurer objects
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLogic.Insurance
{
    using System;
    using System.Linq;
    using System.Security;
    using System.Security.Principal;
    using System.Web.Security;

    using BusinessLogic.Helpers;

    using Data.Repository;

    using Infrastructure.Logging;

    using Ninject;

    using AccountModels = Infrastructure.Model.Account;
    using InsuranceModels = Infrastructure.Model.Insurance;

    /// <summary>
    /// Business logic for working with <seealso cref="InsuranceModels.Insurer"/> objects
    /// </summary>
    public class Insurers
    {
        #region private members

        /// <summary>
        /// The <seealso cref="IReadOnlyRepository{T}"/> for the 
        /// <seealso cref="InsuranceModels.Insurer"/> models
        /// </summary>
        private readonly IReadOnlyRepository<InsuranceModels.Insurer> insurerReadOnlyRepository;

        /// <summary>
        /// The <seealso cref="IRepository{T}"/> for the 
        /// <seealso cref="InsuranceModels.Insurer"/> models
        /// </summary>
        private readonly IRepository<InsuranceModels.Insurer> insurerRepository;

        /// <summary>
        /// The <seealso cref="ILogger{T}"/> to use for logging messages
        /// </summary>
        private readonly ILogger<Insurers> logger;

        /// <summary>
        /// The object that implements the <seealso cref="IKernel"/> interface used for
        /// dependency injection
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Insurers"/> class.
        /// Interfaces are used for initialization to facilitate dependency injection.
        /// </summary>
        /// <param name="insurerReadOnlyRepository">
        /// The <seealso cref="IReadOnlyRepository{T}"/> to use for retrieving 
        /// <seealso cref="InsuranceModels.Insurer"/> records from the repository
        /// </param>
        /// <param name="insurerRepository">
        /// The <seealso cref="IRepository{T}"/> to use for adding, deleting, and updating 
        /// <seealso cref="InsuranceModels.Insurer"/> records in the  repository
        /// </param>
        /// <param name="logger">
        /// The <seealso cref="ILogger{T}"/> to use to log messages
        /// </param>
        /// <param name="kernel">
        /// The <seealso cref="IKernel"/> to use for dependency injection
        /// </param>
        public Insurers(
            IReadOnlyRepository<InsuranceModels.Insurer> insurerReadOnlyRepository,
            IRepository<InsuranceModels.Insurer> insurerRepository,
            ILogger<Insurers> logger,
            IKernel kernel)
        {
            this.insurerReadOnlyRepository = insurerReadOnlyRepository;
            this.insurerRepository = insurerRepository;
            this.logger = logger;
            this.kernel = kernel;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds an insurer to the repository
        /// </summary>
        /// <param name="insurer">
        /// The insurer to add.
        /// </param>
        /// <param name="identity">
        /// The identity of the user authorized to add the insurer to the insurer
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="insurer"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to add the insurer to the
        /// repository
        /// </exception>
        public void CreateInsurer(InsuranceModels.Insurer insurer, IIdentity identity)
        {
            logger.EnterMethod("CreateInsurer");

            Invariant.IsNotNull(insurer, "insurer");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                var user = Membership.GetUser(identity.Name, false);
                var accountReadRepository = kernel.Get<IReadOnlyRepository<AccountModels.Account>>();
                var userAccount = accountReadRepository.FindBy(account => account.UserId.Value.Equals((Guid)user.ProviderUserKey));

                insurer.AccountId = userAccount.Id;

                insurerRepository.Add(insurer);
            }
            catch (Exception exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("CreateInsurer");
        }

        /// <summary>
        /// Deletes an insurer from the repository
        /// </summary>
        /// <param name="insurer">The <seealso cref="InsuranceModels.Insurer"/> to 
        /// delete from the repository</param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to delete the 
        /// <seealso cref="InsuranceModels.Insurer"/> from the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="insurer"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not  authorized to delete the insurer from the repository
        /// </exception>
        public void DeleteInsurer(InsuranceModels.Insurer insurer, IIdentity identity)
        {
            logger.EnterMethod("DeleteInsurer");

            Invariant.IsNotNull(insurer, "insurer");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, insurer.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in DeleteInsurer");
                throw;
            }

            try
            {
                insurerRepository.Delete(insurer);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("DeleteInsurer");
        }

        /// <summary>
        /// Updates an insurer in the repository
        /// </summary>
        /// <param name="insurer">
        /// The <seealso cref="InsuranceModels.Insurer"/> to update in the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to update the 
        /// <seealso cref="InsuranceModels.Insurer"/> in the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="insurer"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to update the insurer in
        /// the repository
        /// </exception>
        public void UpdateInsurer(InsuranceModels.Insurer insurer, IIdentity identity)
        {
            logger.EnterMethod("UpdateInsurer");

            Invariant.IsNotNull(insurer, "insurer");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, insurer.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateInsurer");
                throw;
            }

            try
            {
                insurerRepository.Update(insurer);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("UpdateInsurer");
        }

        #region insurer retrieval

        /// <summary>
        /// Retrieves an <seealso cref="InsuranceModels.Insurer"/> from the 
        /// repository using the id of the insurer
        /// </summary>
        /// <param name="insurerId">
        /// The id of the insurer to retrieve
        /// </param>
        /// <param name="identity">
        /// The identity whose credentials are used to authorize the action
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="insurerId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the insurer in
        /// the repository
        /// </exception>
        /// <returns>
        /// The retrieved <seealso cref="InsuranceModels.Insurer"/> if it is 
        /// found in the repository or null if it is not found
        /// </returns>
        public InsuranceModels.Insurer GetInsurerById(int insurerId, IIdentity identity)
        {
            logger.EnterMethod("GetInsurerById");

            Invariant.IsNotNull(identity, "identity");

            if (insurerId < 1)
            {
                throw new ArgumentOutOfRangeException("insurerId", insurerId, "The insurerId parameter must be greater than 0.");
            }

            var requestedInsurer = insurerReadOnlyRepository.FindBy(insurer => insurer.Id.Equals(insurerId));

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, requestedInsurer.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateInsurer");
                throw;
            }

            logger.LeaveMethod("GetInsurerById");
            return requestedInsurer;
        }

        /// <summary>
        /// Retrives an <seealso cref="IQueryable{T}"/> of <seealso cref="InsuranceModels.Insurer"/> from the repository.
        /// </summary>
        /// <param name="identity">
        /// The identity of the user requesting the insurers.
        /// </param>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> of <seealso cref="InsuranceModels.Insurer"/>.
        /// </returns>
        public IQueryable<InsuranceModels.Insurer> GetInsurers(IIdentity identity)
        {
            logger.EnterMethod("GetInsurers");

            Invariant.IsNotNull(identity, "identity");

            IQueryable<InsuranceModels.Insurer> insurers;

            if (Roles.IsUserInRole(identity.Name, "Admin"))
            {
                insurers = insurerReadOnlyRepository.All();
            }
            else
            {
                var user = Membership.GetUser(identity.Name, false);
                var accountReadRepository = kernel.Get<IReadOnlyRepository<AccountModels.Account>>();
                var userAccount = accountReadRepository.FindBy(account => account.UserId.Value.Equals((Guid)user.ProviderUserKey));

                insurers = insurerReadOnlyRepository.FilterBy(insurer => insurer.AccountId.Equals(userAccount.Id));
            }

            logger.LeaveMethod("GetInsurers");

            return insurers;
        }

        #endregion

        #endregion
    }
}
