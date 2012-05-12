// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationController.cs" company="LC LLC">
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
    public class AuthorizationController : Controller
    {
        /// <summary>
        /// The Ninject DI kernel.
        /// </summary>
        public readonly IKernel Kernel;

        /// <summary>
        /// The <seealso cref="AuthorizationRequests"/> business logic object.
        /// </summary>
        private readonly AuthorizationRequests authorizationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationController"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public AuthorizationController(IKernel kernel)
        {
            Kernel = kernel;
            this.authorizationManager = kernel.Get<AuthorizationRequests>();
        }

        /// <summary>
        /// GET Index action - retrieves all authorization requests for the logged in user or all if the user is an 
        /// administrator.
        /// </summary>
        /// <param name="insurerid">
        /// The insurerid to get the authorization request for.
        /// </param>
        /// <returns>
        /// The Index view that lists the authorization requests.
        /// </returns>
        public ActionResult Index(int? insurerid)
        {
            IQueryable<AuthorizationRequest> requests;

            if (insurerid.HasValue)
            {
                ViewBag.InsurerId = insurerid;

                requests = 
                    authorizationManager
                        .GetAuthorizationRequestsByInsurer(insurerid.Value, HttpContext.User.Identity);
            }
            else
            {
                requests =
                    authorizationManager
                        .GetAuthorizationRequests(HttpContext.User.Identity);
            }

            return View(requests);
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
            return View(authorizationManager.GetAuthorizationRequestById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Create action - returns the Create view used to enter authorization request information.
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurer that this request will be made for.
        /// </param>
        /// <returns>
        /// The Create view.
        /// </returns>
        public ActionResult Create(int insurerid)
        {
            var accountid = Kernel.Get<Accounts>().GetAccountByIdentity(HttpContext.User.Identity).Id;
            var request = new AuthorizationRequest { AccountId = accountid, InsurerId = insurerid };
            return View(request);
        }

        /// <summary>
        /// POST Create action - Creates an authorization request.
        /// </summary>
        /// <param name="request">
        /// The <seealso cref="AuthorizationRequest"/> object to create.
        /// </param>
        /// <returns>
        /// The appropriate view based on the state after the request is created.
        /// </returns>
        [HttpPost]
        public ActionResult Create(AuthorizationRequest request)
        {
            try
            {
                authorizationManager.CreateAuthorizationRequest(request, HttpContext.User.Identity);

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

                return View(request);
            }
        }

        /// <summary>
        /// GET Edit action - Retrieves an <seealso cref="AuthorizationRequest"/> from the repository and displays it
        /// in the edit form.
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="AuthorizationRequest"/> to edit.
        /// </param>
        /// <returns>
        /// The Edit View for authorization requests.
        /// </returns>
        public ActionResult Edit(int id)
        {
            return View(authorizationManager.GetAuthorizationRequestById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Edit action - Accepts the form values for the Authorization Request Edit view and updates the
        /// <seealso cref="AuthorizationRequest"/>.
        /// </summary>
        /// <param name="request">
        /// The <seealso cref="AuthorizationRequest"/> that has been updated.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the success status of the edit.
        /// </returns>
        [HttpPost]
        public ActionResult Edit(AuthorizationRequest request)
        {
            try
            {
                authorizationManager.UpdateAuthorizationRequest(request, HttpContext.User.Identity);
 
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
        /// The id of the <seealso cref="AuthorizationRequest"/> to delete.
        /// </param>
        /// <returns>
        /// The Delete view populated with the data from the requested <seealso cref="AuthorizationRequest"/>
        /// </returns>
        public ActionResult Delete(int id)
        {
            return View(authorizationManager.GetAuthorizationRequestById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Delete action - deletes an <seealso cref="AuthorizationRequest"/> from the repository
        /// </summary>
        /// <param name="request">
        /// The authorization request to delete.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the succcess of the delete action.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(AuthorizationRequest request)
        {
            try
            {
                authorizationManager.DeleteAuthorizationRequest(request, HttpContext.User.Identity);
 
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
