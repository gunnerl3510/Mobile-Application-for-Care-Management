// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FacilityController.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Controller to manage all facility actions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace caremanagement.bradenstrust.org.Controllers
{
    using System;
    using System.Web.Mvc;

    using BusinessLogic.Medical;

    using Infrastructure.Model.Medical;

    using Ninject;

    /// <summary>
    /// Controller to manage all facility actions.
    /// </summary>
    public class FacilityController : Controller
    {
        /// <summary>
        /// The Ninject DI kernel.
        /// </summary>
        public readonly IKernel Kernel;

        /// <summary>
        /// The <seealso cref="Facilities"/> business logic object.
        /// </summary>
        private readonly Facilities facilityManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityController"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public FacilityController(IKernel kernel)
        {
            Kernel = kernel;
            facilityManager = kernel.Get<Facilities>();
        }

        /// <summary>
        /// GET Index action - retrieves all facilities for the logged in user or all if the user is an administrator
        /// </summary>
        /// <returns>
        /// The Index view that lists the facilities.
        /// </returns>
        public ActionResult Index()
        {
            return View(facilityManager.GetFacilities(HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Details - retrieves the data for a <seealso cref="Facility"/> using its ID
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Facility"/>.
        /// </param>
        /// <returns>
        /// The Details view for facilities
        /// </returns>
        public ActionResult Details(int id)
        {
            return View(facilityManager.GetFacilityById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Create action - returns the Create view used to enter facility information.
        /// </summary>
        /// <returns>
        /// The Create view.
        /// </returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST Create action - Creates an facility.
        /// </summary>
        /// <param name="facility">
        /// The <seealso cref="Facility"/> object to create.
        /// </param>
        /// <returns>
        /// The appropriate view based on the state after the facility is created.
        /// </returns>
        [HttpPost]
        public ActionResult Create(Facility facility)
        {
            try
            {
                facilityManager.CreateFacility(facility, HttpContext.User.Identity);

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

                return View(facility);
            }
        }

        /// <summary>
        /// GET Edit action - Retrieves a <seealso cref="Facility"/> from the repository and displays it in the edit form.
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Facility"/> to edit.
        /// </param>
        /// <returns>
        /// The Edit View for facilities.
        /// </returns>
        public ActionResult Edit(int id)
        {
            return View(facilityManager.GetFacilityById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Edit action - Accepts the form values for the Facility Edit view and updates the
        /// <seealso cref="Facility"/>.
        /// </summary>
        /// <param name="facility">
        /// The <seealso cref="Facility"/> that has been updated.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the success status of the edit.
        /// </returns>
        [HttpPost]
        public ActionResult Edit(Facility facility)
        {
            try
            {
                facilityManager.UpdateFacility(facility, HttpContext.User.Identity);
 
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

                return View(facility);
            }
        }

        /// <summary>
        /// GET Delete action - displays the "are you sure" delete view.
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Facility"/> to delete.
        /// </param>
        /// <returns>
        /// The Delete view populated with the data from the requested <seealso cref="Facility"/>
        /// </returns>
        public ActionResult Delete(int id)
        {
            return View(facilityManager.GetFacilityById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Delete action - deletes a <seealso cref="Facility"/> from the repository
        /// </summary>
        /// <param name="facility">
        /// The facility to delete.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the succcess of the delete action.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(Facility facility)
        {
            try
            {
                facilityManager.DeleteFacility(facility, HttpContext.User.Identity);
 
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

                return View(facility);
            }
        }
    }
}
