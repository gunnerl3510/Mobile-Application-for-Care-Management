// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsuranceController.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Controller to manage all insurance actions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace caremanagement.bradenstrust.org.Controllers
{
    using System;
    using System.Web.Mvc;

    using BusinessLogic.Insurance;

    using Infrastructure.Model.Insurance;

    using Ninject;

    /// <summary>
    /// Controller to manage all insurance actions.
    /// </summary>
    public class InsuranceController : Controller
    {
        /// <summary>
        /// The Ninject DI kernel.
        /// </summary>
        public readonly IKernel Kernel;

        /// <summary>
        /// The <seealso cref="Insurers"/> business logic object.
        /// </summary>
        private readonly Insurers insurerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceController"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public InsuranceController(IKernel kernel)
        {
            Kernel = kernel;
            insurerManager = kernel.Get<Insurers>();
        }

        /// <summary>
        /// GET Index action - retrieves all insurances for the logged in user or all if the user is an administrator
        /// </summary>
        /// <returns>
        /// The Index view that lists the insurance organiztions.
        /// </returns>
        public ActionResult Index()
        {
            return View(insurerManager.GetInsurers(HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Details - retrieves the data for an <seealso cref="Insurer"/> using its ID
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Insurer"/>.
        /// </param>
        /// <returns>
        /// The Details view for insurers
        /// </returns>
        public ActionResult Details(int id)
        {
            return View(insurerManager.GetInsurerById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Create action - returns the Create view used to enter insurer information.
        /// </summary>
        /// <returns>
        /// The Create view.
        /// </returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST Create action - Creates an insurer.
        /// </summary>
        /// <param name="insurer">
        /// The <seealso cref="Insurer"/> object to create.
        /// </param>
        /// <returns>
        /// The appropriate view based on the state after the insurer is created.
        /// </returns>
        [HttpPost]
        public ActionResult Create(Insurer insurer)
        {
            try
            {
                insurerManager.CreateInsurer(insurer, HttpContext.User.Identity);

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
                    ViewBag.InnerErrorMessage = string.Format("The inner message is: {0}", exception.InnerException.Message);
                }

                return View(insurer);
            }
        }

        /// <summary>
        /// GET Edit action - Retrieves an <seealso cref="Insurer"/> from the repository and displays it in the edit form.
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Insurer"/> to edit.
        /// </param>
        /// <returns>
        /// The Edit View for insurers.
        /// </returns>
        public ActionResult Edit(int id)
        {
            return View(insurerManager.GetInsurerById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Edit action - Accepts the form values for the Insurer Edit view and updates the
        /// <seealso cref="Insurer"/>.
        /// </summary>
        /// <param name="insurer">
        /// The <seealso cref="Insurer"/> that has been updated.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the success status of the edit.
        /// </returns>
        [HttpPost]
        public ActionResult Edit(Insurer insurer)
        {
            try
            {
                insurerManager.UpdateInsurer(insurer, HttpContext.User.Identity);
 
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
                    ViewBag.InnerErrorMessage = string.Format("The inner message is: {0}", exception.InnerException.Message);
                }

                return View(insurer);
            }
        }

        /// <summary>
        /// GET Delete action - displays the "are you sure" delete view.
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Insurer"/> to delete.
        /// </param>
        /// <returns>
        /// The Delete view populated with the data from the requested <seealso cref="Insurer"/>
        /// </returns>
        public ActionResult Delete(int id)
        {
            return View(insurerManager.GetInsurerById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Delete action - deletes an <seealso cref="Insurer"/> from the repository
        /// </summary>
        /// <param name="insurer">
        /// The insurer to delete.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the succcess of the delete action.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(Insurer insurer)
        {
            try
            {
                insurerManager.DeleteInsurer(insurer, HttpContext.User.Identity);
 
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
                    ViewBag.InnerErrorMessage = string.Format("The inner message is: {0}", exception.InnerException.Message);
                }

                return View(insurer);
            }
        }
    }
}
