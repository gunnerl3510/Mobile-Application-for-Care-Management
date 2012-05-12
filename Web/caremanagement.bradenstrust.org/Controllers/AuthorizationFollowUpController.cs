// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationFollowUpController.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Controller to manage all insurance authorization actions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace caremanagement.bradenstrust.org.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using BusinessLogic.Account;
    using BusinessLogic.Insurance;

    using Infrastructure.Model.Insurance;

    using Ninject;

    /// <summary>
    /// Controller to manage all insurance authorization actions.
    /// </summary>
    [Authorize]
    public class AuthorizationFollowUpController : Controller
    {
        /// <summary>
        /// The Ninject DI kernel.
        /// </summary>
        public readonly IKernel Kernel;

        /// <summary>
        /// The <seealso cref="AuthorizationFollowUp"/> business logic object.
        /// </summary>
        private readonly AuthorizationFollowUps authorizationFollowUpManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationFollowUpController"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public AuthorizationFollowUpController(IKernel kernel)
        {
            Kernel = kernel;
            this.authorizationFollowUpManager = kernel.Get<AuthorizationFollowUps>();
        }

        /// <summary>
        /// GET Index action - retrieves all authorization follow ups for the logged in user or all if the user is an 
        /// administrator.
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurance orgnanization that the authorization request belongs to
        /// </param>
        /// <param name="authorizationid">
        /// The id of the authorization to get the follow up for.
        /// </param>
        /// <returns>
        /// The Index view that lists the authorization follow ups.
        /// </returns>
        public ActionResult Index(int? insurerid, int? authorizationid)
        {
            IQueryable<AuthorizationFollowUp> followUps;

            if (authorizationid.HasValue)
            {
                ViewBag.AuthorizationId = authorizationid.Value;

                if (insurerid.HasValue)
                {
                    ViewBag.InsuranceId = insurerid.Value;
                }

                followUps =
                    authorizationFollowUpManager
                        .GetAuthorizationFollowUpsByAuthorizationRequest(authorizationid.Value, HttpContext.User.Identity);
            }
            else
            {
                followUps = authorizationFollowUpManager.GetAuthorizationFollowUps(HttpContext.User.Identity);
            }

            return View(followUps);
        }

        /// <summary>
        /// GET Details - retrieves the data for an <seealso cref="AuthorizationRequest"/> using its ID
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="AuthorizationRequest"/>.
        /// </param>
        /// <returns>
        /// The Details view for authorization requests
        /// </returns>
        public ActionResult Details(int id)
        {
            return View(authorizationFollowUpManager.GetAuthorizationFollowUpById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Create action - returns the Create view used to enter authorization follow up information.
        /// </summary>
        /// <param name="authorizationid">
        /// The id of the authorization that this follow up will be made for.
        /// </param>
        /// <returns>
        /// The Create view.
        /// </returns>
        public ActionResult Create(int authorizationid)
        {
            var accountid = Kernel.Get<Accounts>().GetAccountByIdentity(HttpContext.User.Identity).Id;
            var request = new AuthorizationFollowUp
                {
                    AccountId = accountid, AuthorizationRequestId = authorizationid 
                };
            return View(request);
        }

        /// <summary>
        /// POST Create action - Creates an authorization follow up.
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurer that the authorization follow up belongs to.
        /// </param>
        /// <param name="followUp">
        /// The  <seealso cref="AuthorizationFollowUp"/> object to create.
        /// </param>
        /// <returns>
        /// The appropriate view based on the state after the follow up is created.
        /// </returns>
        [HttpPost]
        public ActionResult Create(int? insurerid, AuthorizationFollowUp followUp)
        {
            if (insurerid.HasValue)
            {
                ViewBag.InsurerId = insurerid;
            }

            try
            {
                authorizationFollowUpManager.CreateAuthorizationFollowUp(followUp, HttpContext.User.Identity);

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

                return View(followUp);
            }
        }

        /// <summary>
        /// GET Edit action - Retrieves an <seealso cref="AuthorizationFollowUp"/> from the repository and displays it
        /// in the edit form.
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurer that the authorization request belongs to.
        /// </param>
        /// <param name="id">
        /// The id of the <seealso cref="AuthorizationFollowUp"/> to edit.
        /// </param>
        /// <returns>
        /// The Edit View for authorization follow ups.
        /// </returns>
        public ActionResult Edit(int? insurerid, int id)
        {
            if (insurerid.HasValue)
            {
                ViewBag.InsurerId = insurerid;
            }

            return View(authorizationFollowUpManager.GetAuthorizationFollowUpById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Edit action - Accepts the form values for the Authorization Follow Up Edit view and updates the
        /// <seealso cref="AuthorizationFollowUp"/>.
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurer that the authorization request belongs to.
        /// </param>
        /// <param name="followUp">
        /// The <seealso cref="AuthorizationFollowUp"/>
        /// </param>
        /// <returns>
        /// The appropriate view determined by the success status of the edit.
        /// </returns>
        [HttpPost]
        public ActionResult Edit(int? insurerid, AuthorizationFollowUp followUp)
        {
            try
            {
                authorizationFollowUpManager.UpdateAuthorizationFollowUp(followUp, HttpContext.User.Identity);
 
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

                if (insurerid.HasValue)
                {
                    ViewBag.InsurerId = insurerid;
                }

                return View(followUp);
            }
        }

        /// <summary>
        /// GET Delete action - displays the "are you sure" delete view.
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurer that the authorization request belongs to.
        /// </param>
        /// <param name="id">
        /// The id of the <seealso cref="AuthorizationFollowUp"/> to delete.
        /// </param>
        /// <returns>
        /// The Delete view populated with the data from the requested <seealso cref="AuthorizationFollowUp"/>
        /// </returns>
        public ActionResult Delete(int? insurerid, int id)
        {
            if (insurerid.HasValue)
            {
                ViewBag.InsurerId = insurerid;
            }

            return View(authorizationFollowUpManager.GetAuthorizationFollowUpById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Delete action - deletes an <seealso cref="AuthorizationFollowUp"/> from the repository
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurer that the authorization request belongs to.
        /// </param>
        /// <param name="followUp">
        /// The <seealso cref="AuthorizationFollowUp"/> to delete.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the succcess of the delete action.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(int? insurerid, AuthorizationFollowUp followUp)
        {
            try
            {
                authorizationFollowUpManager.DeleteAuthorizationFollowUp(followUp, HttpContext.User.Identity);
 
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

                if (insurerid.HasValue)
                {
                    ViewBag.InsurerId = insurerid;
                }

                return View(followUp);
            }
        }
    }
}
