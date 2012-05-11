// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedicationController.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Controller to manage all medication actions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace caremanagement.bradenstrust.org.Controllers
{
    using System;
    using System.Web.Mvc;

    using BusinessLogic.Prescription;

    using Infrastructure.Model.Prescription;

    using Ninject;

    /// <summary>
    /// Controller to manage all medication actions.
    /// </summary>
    public class MedicationController : Controller
    {
        /// <summary>
        /// The Ninject DI kernel.
        /// </summary>
        public readonly IKernel Kernel;

        /// <summary>
        /// The <seealso cref="Medications"/> business logic object.
        /// </summary>
        private readonly Medications medicationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationController"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public MedicationController(IKernel kernel)
        {
            Kernel = kernel;
            medicationManager = kernel.Get<Medications>();
        }

        /// <summary>
        /// GET Index action - retrieves all medications for the logged in user or all if the user is an administrator
        /// </summary>
        /// <returns>
        /// The Index view that lists the medications.
        /// </returns>
        public ActionResult Index()
        {
            return View(medicationManager.GetMedications(HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Details - retrieves the data for a <seealso cref="Medication"/> using its ID
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Medication"/>.
        /// </param>
        /// <returns>
        /// The Details view for medications.
        /// </returns>
        public ActionResult Details(int id)
        {
            return View(medicationManager.GetMedicationById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Create action - returns the Create view used to enter medication information.
        /// </summary>
        /// <returns>
        /// The Create view.
        /// </returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST Create action - Creates a medication.
        /// </summary>
        /// <param name="medication">
        /// The <seealso cref="Medication"/> object to create.
        /// </param>
        /// <returns>
        /// The appropriate view based on the state after the medication is created.
        /// </returns>
        [HttpPost]
        public ActionResult Create(Medication medication)
        {
            try
            {
                medicationManager.CreateMedication(medication, HttpContext.User.Identity);

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

                return View(medication);
            }
        }

        /// <summary>
        /// GET Edit action - Retrieves a <seealso cref="Medication"/> from the repository and displays it in the edit form.
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Medication"/> to edit.
        /// </param>
        /// <returns>
        /// The Edit View for medications.
        /// </returns>
        public ActionResult Edit(int id)
        {
            return View(medicationManager.GetMedicationById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Edit action - Accepts the form values for the Medication Edit view and updates the
        /// <seealso cref="Medication"/>.
        /// </summary>
        /// <param name="medication">
        /// The <seealso cref="Medication"/> that has been updated.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the success status of the edit.
        /// </returns>
        [HttpPost]
        public ActionResult Edit(Medication medication)
        {
            try
            {
                medicationManager.UpdateMedication(medication, HttpContext.User.Identity);
 
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

                return View(medication);
            }
        }

        /// <summary>
        /// GET Delete action - displays the "are you sure" delete view.
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="Medication"/> to delete.
        /// </param>
        /// <returns>
        /// The Delete view populated with the data from the requested <seealso cref="Medication"/>
        /// </returns>
        public ActionResult Delete(int id)
        {
            return View(medicationManager.GetMedicationById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Delete action - deletes a <seealso cref="Medication"/> from the repository
        /// </summary>
        /// <param name="medication">
        /// The medication to delete.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the succcess of the delete action.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(Medication medication)
        {
            try
            {
                medicationManager.DeleteMedication(medication, HttpContext.User.Identity);
 
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

                return View(medication);
            }
        }
    }
}
