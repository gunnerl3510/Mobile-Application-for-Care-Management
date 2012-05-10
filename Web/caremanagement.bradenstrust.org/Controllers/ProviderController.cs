// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProviderController.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Controller to manage all provider actions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace caremanagement.bradenstrust.org.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using BusinessLogic.Account;
    using BusinessLogic.Medical;

    using Infrastructure.Model.Medical;

    using Ninject;

    /// <summary>
    /// Controller to manage all provider actions.
    /// </summary>
    public class ProviderController : Controller
    {
        /// <summary>
        /// The Ninject DI kernel.
        /// </summary>
        public readonly IKernel Kernel;

        /// <summary>
        /// The <seealso cref="Providers"/> business logic object.
        /// </summary>
        private readonly Providers providerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderController"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public ProviderController(IKernel kernel)
        {
            Kernel = kernel;
            this.providerManager = kernel.Get<Providers>();
        }

        /// <summary>
        /// GET Index action - retrieves all providers for the logged in user or all if the user is an 
        /// administrator.
        /// </summary>
        /// <param name="facilityid">
        /// The facilityid to get the provider for.
        /// </param>
        /// <returns>
        /// The Index view that lists the providers.
        /// </returns>
        public ActionResult Index(int? facilityid)
        {
            IQueryable<Provider> providers;

            if (facilityid.HasValue)
            {
                ViewBag.FacilityId = facilityid;

                providers = 
                    this.providerManager
                        .GetProvidersByFacility(facilityid.Value, HttpContext.User.Identity);
            }
            else
            {
                providers =
                    this.providerManager
                        .GetProviders(HttpContext.User.Identity);
            }

            return View(providers);
        }

        /// <summary>
        /// GET Details - retrieves the data for a <seealso cref="Provider"/> using its ID
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Provider"/>.
        /// </param>
        /// <returns>
        /// The Details view for providers
        /// </returns>
        public ActionResult Details(int id)
        {
            return View(this.providerManager.GetProviderById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Create action - returns the Create view used to enter provider information.
        /// </summary>
        /// <param name="facilityid">
        /// The id of the facility that this provider works for.
        /// </param>
        /// <returns>
        /// The Create view.
        /// </returns>
        public ActionResult Create(int facilityid)
        {
            var accountid = Kernel.Get<Accounts>().GetAccountByIdentity(HttpContext.User.Identity).Id;
            var provider = new Provider { AccountId = accountid, FacilityId = facilityid };
            return View(provider);
        }

        /// <summary>
        /// POST Create action - Creates a provider.
        /// </summary>
        /// <param name="provider">
        /// The <seealso cref="Provider"/> object to create.
        /// </param>
        /// <returns>
        /// The appropriate view based on the state after the provider is created.
        /// </returns>
        [HttpPost]
        public ActionResult Create(Provider provider)
        {
            try
            {
                this.providerManager.CreateProvider(provider, HttpContext.User.Identity);

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ViewBag.ErrorMessage =
                    string.Format(
                        "There was an error while trying to process your action.  The message is: {0}",
                        exception.Message);
                if (exception.InnerException != null)
                {
                    ViewBag.InnerErrorMessage = string.Format(
                        "The inner message is: {0}", exception.InnerException.Message);
                }

                return View(provider);
            }
        }

        /// <summary>
        /// GET Edit action - Retrieves a <seealso cref="Provider"/> from the repository and displays it
        /// in the edit form.
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Provider"/> to edit.
        /// </param>
        /// <returns>
        /// The Edit View for providers.
        /// </returns>
        public ActionResult Edit(int id)
        {
            return View(this.providerManager.GetProviderById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Edit action - Accepts the form values for the Authorization Request Edit view and updates the
        /// <seealso cref="Provider"/>.
        /// </summary>
        /// <param name="provider">
        /// The <seealso cref="Provider"/> that has been updated.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the success status of the edit.
        /// </returns>
        [HttpPost]
        public ActionResult Edit(Provider provider)
        {
            try
            {
                this.providerManager.UpdateProvider(provider, HttpContext.User.Identity);
 
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ViewBag.ErrorMessage =
                    string.Format(
                        "There was an error while trying to process your action.  The message is: {0}",
                        exception.Message);
                if (exception.InnerException != null)
                {
                    ViewBag.InnerErrorMessage = string.Format(
                        "The inner message is: {0}", exception.InnerException.Message);
                }

                return View();
            }
        }

        /// <summary>
        /// GET Delete action - displays the "are you sure" delete view.
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Provider"/> to delete.
        /// </param>
        /// <returns>
        /// The Delete view populated with the data from the requested <seealso cref="Provider"/>
        /// </returns>
        public ActionResult Delete(int id)
        {
            return View(this.providerManager.GetProviderById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Delete action - deletes a <seealso cref="Provider"/> from the repository
        /// </summary>
        /// <param name="provider">
        /// The provider to delete.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the succcess of the delete action.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(Provider provider)
        {
            try
            {
                this.providerManager.DeleteProvider(provider, HttpContext.User.Identity);
 
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ViewBag.ErrorMessage =
                    string.Format(
                        "There was an error while trying to process your action.  The message is: {0}",
                        exception.Message);
                if (exception.InnerException != null)
                {
                    ViewBag.InnerErrorMessage = string.Format(
                        "The inner message is: {0}", exception.InnerException.Message);
                }

                return View();
            }
        }
    }
}
