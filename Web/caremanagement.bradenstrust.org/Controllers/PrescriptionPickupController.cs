// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrescriptionPickupController.cs" company="LC LLC">
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
    using BusinessLogic.Prescription;

    using Infrastructure.Model.Prescription;

    using Ninject;

    /// <summary>
    /// Controller to manage all insurance authorization actions.
    /// </summary>
    public class PrescriptionPickupController : Controller
    {
        /// <summary>
        /// The Ninject DI kernel.
        /// </summary>
        public readonly IKernel Kernel;

        /// <summary>
        /// The <seealso cref="PrescriptionPickups"/> business logic object.
        /// </summary>
        private readonly PrescriptionPickups pickupManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrescriptionPickupController"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public PrescriptionPickupController(IKernel kernel)
        {
            Kernel = kernel;
            pickupManager = kernel.Get<PrescriptionPickups>();
        }

        /// <summary>
        /// GET Index action - retrieves all prescription pickups for the logged in user or all if the user is an 
        /// administrator.
        /// </summary>
        /// <param name="medicationid">
        /// The medication id to get the prescription pickup for.
        /// </param>
        /// <returns>
        /// The Index view that lists the prescription pickups.
        /// </returns>
        public ActionResult Index(int? medicationid)
        {
            IQueryable<PrescriptionPickup> pickups;

            if (medicationid.HasValue)
            {
                ViewBag.MedicationId = medicationid;

                pickups = 
                    pickupManager
                        .GetPrescriptionPickupsByMedication(medicationid.Value, HttpContext.User.Identity);
            }
            else
            {
                pickups =
                    pickupManager
                        .GetPrescriptionPickups(HttpContext.User.Identity);
            }

            return View(pickups);
        }

        /// <summary>
        /// GET Details - retrieves the data for a <seealso cref="PrescriptionPickup"/> using its ID
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="PrescriptionPickup"/>.
        /// </param>
        /// <returns>
        /// The Details view for prescription pickups
        /// </returns>
        public ActionResult Details(int id)
        {
            return View(pickupManager.GetPrescriptionPickupById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Create action - returns the Create view used to enter prescription pickup information.
        /// </summary>
        /// <param name="medicationid">
        /// The id of the medication that this pickup will be made for.
        /// </param>
        /// <returns>
        /// The Create view.
        /// </returns>
        public ActionResult Create(int medicationid)
        {
            var accountid = Kernel.Get<Accounts>().GetAccountByIdentity(HttpContext.User.Identity).Id;
            var pickup = new PrescriptionPickup { AccountId = accountid, MedicationId = medicationid };
            return View(pickup);
        }

        /// <summary>
        /// POST Create action - Creates a prescription pickup.
        /// </summary>
        /// <param name="pickup">
        /// The <seealso cref="PrescriptionPickup"/> object to create.
        /// </param>
        /// <returns>
        /// The appropriate view based on the state after the pickup is created.
        /// </returns>
        [HttpPost]
        public ActionResult Create(PrescriptionPickup pickup)
        {
            try
            {
                pickupManager.CreatePrescriptionPickup(pickup, HttpContext.User.Identity);

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

                return View(pickup);
            }
        }

        /// <summary>
        /// GET Edit action - Retrieves a <seealso cref="PrescriptionPickup"/> from the repository and displays it
        /// in the edit form.
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="PrescriptionPickup"/> to edit.
        /// </param>
        /// <returns>
        /// The Edit View for prescription pickups.
        /// </returns>
        public ActionResult Edit(int id)
        {
            return View(pickupManager.GetPrescriptionPickupById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Edit action - Accepts the form values for the Authorization Request Edit view and updates the
        /// <seealso cref="PrescriptionPickup"/>.
        /// </summary>
        /// <param name="pickup">
        /// The <seealso cref="PrescriptionPickup"/> that has been updated.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the success status of the edit.
        /// </returns>
        [HttpPost]
        public ActionResult Edit(PrescriptionPickup pickup)
        {
            try
            {
                pickupManager.UpdatePrescriptionPickup(pickup, HttpContext.User.Identity);
 
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
        /// The id of the <seealso cref="PrescriptionPickup"/> to delete.
        /// </param>
        /// <returns>
        /// The Delete view populated with the data from the requested <seealso cref="PrescriptionPickup"/>
        /// </returns>
        public ActionResult Delete(int id)
        {
            return View(pickupManager.GetPrescriptionPickupById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Delete action - deletes a <seealso cref="PrescriptionPickup"/> from the repository
        /// </summary>
        /// <param name="pickup">
        /// The prescription pickup to delete.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the succcess of the delete action.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(PrescriptionPickup pickup)
        {
            try
            {
                pickupManager.DeletePrescriptionPickup(pickup, HttpContext.User.Identity);
 
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
