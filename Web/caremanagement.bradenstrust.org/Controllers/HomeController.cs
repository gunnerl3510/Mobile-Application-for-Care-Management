// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   The home controller for basic system actions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace caremanagement.bradenstrust.org.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The home controller for basic system actions.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// GET Index action - the default controller and action.
        /// </summary>
        /// <returns>
        /// The default home view.
        /// </returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the Care Management System";

            return View();
        }
    }
}
