// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedicalAppointmentController.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Controller to manage all medical appointments.
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
    /// Controller to manage all medical appointments.
    /// </summary>
    public class MedicalAppointmentController : Controller
    {
        /// <summary>
        /// The Ninject DI kernel.
        /// </summary>
        public readonly IKernel Kernel;

        /// <summary>
        /// The <seealso cref="MedicalAppointment"/> business logic object.
        /// </summary>
        private readonly MedicalAppointments appointmentManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicalAppointmentController"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The kernel.
        /// </param>
        public MedicalAppointmentController(IKernel kernel)
        {
            Kernel = kernel;
            this.appointmentManager = kernel.Get<MedicalAppointments>();
        }

        /// <summary>
        /// GET Index action - retrieves all appointments for the logged in user or all if the user is an 
        /// administrator.
        /// </summary>
        /// <param name="facilityid">
        /// The id of the facility that the provider belongs to
        /// </param>
        /// <param name="providerid">
        /// The id of the provider to get the appointment for.
        /// </param>
        /// <returns>
        /// The Index view that lists the appointments.
        /// </returns>
        public ActionResult Index(int? facilityid, int? providerid)
        {
            IQueryable<MedicalAppointment> appointments;

            if (providerid.HasValue)
            {
                ViewBag.ProviderId = providerid.Value;

                if (facilityid.HasValue)
                {
                    ViewBag.FacilityId = facilityid.Value;
                }

                appointments =
                    this.appointmentManager
                        .GetMedicalAppointmentsByProvider(providerid.Value, HttpContext.User.Identity);
            }
            else
            {
                appointments = this.appointmentManager.GetMedicalAppointments(HttpContext.User.Identity);
            }

            return View(appointments);
        }

        /// <summary>
        /// GET Details - retrieves the data for a <seealso cref="MedicalAppointment"/> using its ID
        /// </summary>
        /// <param name="id">
        /// The id of the <seealso cref="MedicalAppointment"/>.
        /// </param>
        /// <returns>
        /// The Details view for appointments.
        /// </returns>
        public ActionResult Details(int id)
        {
            return View(this.appointmentManager.GetMedicalAppointmentById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// GET Create action - returns the Create view used to enter appointment information.
        /// </summary>
        /// <param name="providerid">
        /// The id of the provider that this appointment will be made for.
        /// </param>
        /// <returns>
        /// The Create view.
        /// </returns>
        public ActionResult Create(int providerid)
        {
            var accountid = Kernel.Get<Accounts>().GetAccountByIdentity(HttpContext.User.Identity).Id;
            var appointment = new MedicalAppointment
                {
                    AccountId = accountid, ProviderId = providerid 
                };
            return View(appointment);
        }

        /// <summary>
        /// POST Create action - Creates a provider.
        /// </summary>
        /// <param name="facilityid">
        /// The id of the facility that the provider belongs to.
        /// </param>
        /// <param name="appointment">
        /// The  <seealso cref="MedicalAppointment"/> object to create.
        /// </param>
        /// <returns>
        /// The appropriate view based on the state after the appointment is created.
        /// </returns>
        [HttpPost]
        public ActionResult Create(int? facilityid, MedicalAppointment appointment)
        {
            if (facilityid.HasValue)
            {
                ViewBag.FacilityId = facilityid;
            }

            try
            {
                appointmentManager.CreateMedicalAppointment(appointment, HttpContext.User.Identity);
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

                return View(appointment);
            }
        }

        /// <summary>
        /// GET Edit action - Retrieves a <seealso cref="MedicalAppointment"/> from the repository and displays it
        /// in the edit form.
        /// </summary>
        /// <param name="facilityid">
        /// The id of the facility that the provider belongs to.
        /// </param>
        /// <param name="id">
        /// The id of the <seealso cref="MedicalAppointment"/> to edit.
        /// </param>
        /// <returns>
        /// The Edit View for appointments.
        /// </returns>
        public ActionResult Edit(int? facilityid, int id)
        {
            if (facilityid.HasValue)
            {
                ViewBag.FacilityId = facilityid;
            }

            return View(this.appointmentManager.GetMedicalAppointmentById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Edit action - Accepts the form values for the Medical Appointment Edit view and updates the
        /// <seealso cref="MedicalAppointment"/>.
        /// </summary>
        /// <param name="facilityid">
        /// The id of the facility that the provider belongs to.
        /// </param>
        /// <param name="appointment">
        /// The <seealso cref="MedicalAppointment"/>
        /// </param>
        /// <returns>
        /// The appropriate view determined by the success status of the edit.
        /// </returns>
        [HttpPost]
        public ActionResult Edit(int? facilityid, MedicalAppointment appointment)
        {
            try
            {
                this.appointmentManager.UpdateMedicalAppointment(appointment, HttpContext.User.Identity);
 
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

                if (facilityid.HasValue)
                {
                    ViewBag.FacilityId = facilityid;
                }

                return View(appointment);
            }
        }

        /// <summary>
        /// GET Delete action - displays the "are you sure" delete view.
        /// </summary>
        /// <param name="facilityid">
        /// The id of the facility that the provider belongs to.
        /// </param>
        /// <param name="id">
        /// The id of the <seealso cref="MedicalAppointment"/> to delete.
        /// </param>
        /// <returns>
        /// The Delete view populated with the data from the requested <seealso cref="MedicalAppointment"/>
        /// </returns>
        public ActionResult Delete(int? facilityid, int id)
        {
            if (facilityid.HasValue)
            {
                ViewBag.FacilityId = facilityid;
            }

            return View(this.appointmentManager.GetMedicalAppointmentById(id, HttpContext.User.Identity));
        }

        /// <summary>
        /// POST Delete action - deletes a <seealso cref="MedicalAppointment"/> from the repository
        /// </summary>
        /// <param name="facilityid">
        /// The id of the facility that the provider belongs to.
        /// </param>
        /// <param name="appointment">
        /// The <seealso cref="MedicalAppointment"/> to delete.
        /// </param>
        /// <returns>
        /// The appropriate view determined by the succcess of the delete action.
        /// </returns>
        [HttpPost]
        public ActionResult Delete(int? facilityid, MedicalAppointment appointment)
        {
            try
            {
                this.appointmentManager.DeleteMedicalAppointment(appointment, HttpContext.User.Identity);
 
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

                if (facilityid.HasValue)
                {
                    ViewBag.FacilityId = facilityid;
                }

                return View(appointment);
            }
        }
    }
}
