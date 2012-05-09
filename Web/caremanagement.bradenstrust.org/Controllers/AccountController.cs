// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Controller for accounts views
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace caremanagement.bradenstrust.org.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Security;

    using BusinessLogic.Account;

    using caremanagement.bradenstrust.org.Models;

    using Ninject;

    /// <summary>
    /// Controller for accounts views
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// The Ninject DI kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="kernel">
        /// The Ninject kernel.
        /// </param>
        public AccountController(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// GET LogOn action
        /// </summary>
        /// <returns>
        /// The LogOn view
        /// </returns>
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// POST LogOn action
        /// </summary>
        /// <param name="viewModel">
        /// The <seealso cref="LogOnViewModel"/> model.
        /// </param>
        /// <param name="returnUrl">
        /// The return url for log on redirects.
        /// </param>
        /// <returns>
        /// The approprite view or redirect depending on the log on state and source of the request.
        /// </returns>
        [HttpPost]
        public ActionResult LogOn(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.UserName, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.UserName, viewModel.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(viewModel);
        }

        /// <summary>
        /// GET LogOff action for logging the current user out of the system.
        /// </summary>
        /// <returns>
        /// A <seealso cref="RedirectToRouteResult"/> back to the default home page.
        /// </returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// GET Register action for registering a new user.
        /// </summary>
        /// <returns>
        /// The Register view.
        /// </returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// POST Register action for registering a new user.
        /// </summary>
        /// <param name="model">
        /// The <seealso cref="RegistrationViewModel"/> model to register.
        /// </param>
        /// <returns>
        /// The appropriate view based on the results of the registration actions.
        /// </returns>
        [HttpPost]
        public ActionResult Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var accountManager = kernel.Get<Accounts>();

                    accountManager.AddAccount(model.Account, model.Password);

                    FormsAuthentication.SetAuthCookie(model.Account.Name, false /* createPersistentCookie */);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
