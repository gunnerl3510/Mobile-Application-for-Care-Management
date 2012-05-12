// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationNoteController.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Controller to manage all insurance authorization notes.
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
    /// Controller to manage all insurance authorization notes.
    /// </summary>
    [Authorize]
    public class AuthorizationNoteController : Controller
    {
        /// <summary>
        /// The Ninject DI kernel.
        /// </summary>
        public readonly IKernel Kernel;

        /// <summary>
        /// The <seealso cref="AuthorizationNote"/> business logic object.
        /// </summary>
        private readonly AuthorizationNotes authorizationNoteManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationNoteController"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public AuthorizationNoteController(IKernel kernel)
        {
            Kernel = kernel;
            this.authorizationNoteManager = kernel.Get<AuthorizationNotes>();
        }

        /// <summary>
        /// GET Index action - retrieves all authorization notes for the logged in user or all if the user is an 
        /// administrator.
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurance orgnanization that the authorization note belongs to
        /// </param>
        /// <param name="authorizationid">
        /// The id of the authorization to get the note for.
        /// </param>
        /// <returns>
        /// The Index view that lists the authorization notes.
        /// </returns>
        public ActionResult Index(int? insurerid, int? authorizationid)
        {
            IQueryable<AuthorizationNote> notes;

            if (authorizationid.HasValue)
            {
                ViewBag.AuthorizationId = authorizationid.Value;

                if (insurerid.HasValue)
                {
                    ViewBag.InsuranceId = insurerid.Value;
                }

                notes =
                    authorizationNoteManager
                        .GetAuthorizationNotesByAuthorizationRequest(authorizationid.Value, HttpContext.User.Identity);
            }
            else
            {
                notes = authorizationNoteManager.GetAuthorizationNotes(HttpContext.User.Identity);
            }

            return View(notes);
        }

        /// <summary>
        /// GET Details - retrieves the data for an <seealso cref="AuthorizationNote"/> using its ID
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="AuthorizationNote"/>.
        /// </param>
        /// <returns>
        /// The Details view for authorization notes.
        /// </returns>
        public ActionResult Details(int id)
        {
            return View(authorizationNoteManager.GetAuthorizationNoteById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Create action - returns the Create view used to enter authorization note information.
        /// </summary>
        /// <param name="authorizationid">
        /// The id of the authorization that this note will be made for.
        /// </param>
        /// <returns>
        /// The Create view.
        /// </returns>
        public ActionResult Create(int authorizationid)
        {
            var accountid = Kernel.Get<Accounts>().GetAccountByIdentity(HttpContext.User.Identity).Id;
            var note = new AuthorizationNote
                {
                    AccountId = accountid, AuthorizationRequestId = authorizationid 
                };
            return View(note);
        }

        /// <summary>
        /// POST Create action - Creates an authorization note.
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurer that the authorization note belongs to.
        /// </param>
        /// <param name="followUp">
        /// The  <seealso cref="AuthorizationNote"/> object to create.
        /// </param>
        /// <returns>
        /// The appropriate view based on the state after the note is created.
        /// </returns>
        [HttpPost]
        public ActionResult Create(int? insurerid, AuthorizationNote followUp)
        {
            if (insurerid.HasValue)
            {
                ViewBag.InsurerId = insurerid;
            }

            try
            {
                followUp.Created = DateTime.UtcNow;
                authorizationNoteManager.CreateAuthorizationNote(followUp, HttpContext.User.Identity);

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
        /// GET Edit action - Retrieves an <seealso cref="AuthorizationNote"/> from the repository and displays it
        /// in the edit form.
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurer that the authorization note belongs to.
        /// </param>
        /// <param name="id">
        /// The id of the <seealso cref="AuthorizationNote"/> to edit.
        /// </param>
        /// <returns>
        /// The Edit View for authorization notes.
        /// </returns>
        public ActionResult Edit(int? insurerid, int id)
        {
            if (insurerid.HasValue)
            {
                ViewBag.InsurerId = insurerid;
            }

            return View(authorizationNoteManager.GetAuthorizationNoteById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Edit action - Accepts the form values for the Authorization Follow Up Edit view and updates the
        /// <seealso cref="AuthorizationNote"/>.
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurer that the authorization note belongs to.
        /// </param>
        /// <param name="followUp">
        /// The <seealso cref="AuthorizationNote"/>
        /// </param>
        /// <returns>
        /// The appropriate view determined by the success status of the edit.
        /// </returns>
        [HttpPost]
        public ActionResult Edit(int? insurerid, AuthorizationNote followUp)
        {
            try
            {
                authorizationNoteManager.UpdateAuthorizationNote(followUp, HttpContext.User.Identity);
 
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
        /// The id of the insurer that the authorization note belongs to.
        /// </param>
        /// <param name="id">
        /// The id of the <seealso cref="AuthorizationNote"/> to delete.
        /// </param>
        /// <returns>
        /// The Delete view populated with the data from the requested <seealso cref="AuthorizationNote"/>
        /// </returns>
        public ActionResult Delete(int? insurerid, int id)
        {
            if (insurerid.HasValue)
            {
                ViewBag.InsurerId = insurerid;
            }

            return View(authorizationNoteManager.GetAuthorizationNoteById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Delete action - deletes an <seealso cref="AuthorizationNote"/> from the repository
        /// </summary>
        /// <param name="insurerid">
        /// The id of the insurer that the authorization note belongs to.
        /// </param>
        /// <param name="followUp">
        /// The <seealso cref="AuthorizationNote"/> to delete.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the succcess of the delete action.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(int? insurerid, AuthorizationNote followUp)
        {
            try
            {
                authorizationNoteManager.DeleteAuthorizationNote(followUp, HttpContext.User.Identity);
 
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
