using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace caremanagement.bradenstrust.org
{
    using System.Configuration;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "MedicalAppointments",
                "Facility/{facilityid}/Provider/{providerid}/MedicalAppointment/{action}/{id}",
                new { controller = "MedicalAppointment", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "AuthorizationFollowUps",
                "Insurance/{insurerid}/Authorization/{authorizationid}/AuthorizationFollowUp/{action}/{id}",
                new { controller = "AuthorizationFollowUp", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "AuthorizationNotes",
                "Insurance/{insurerid}/Authorization/{authorizationid}/AuthorizationNote/{action}/{id}",
                new { controller = "AuthorizationNote", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "Providers",
                "Facility/{facilityid}/Provider/{action}/{id}",
                new { controller = "Provider", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "InsurerAuthorizations",
                "Insurance/{insurerid}/Authorization/{action}/{id}",
                new { controller = "Authorization", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            if (Membership.GetUser("Admin") == null)
            {
                Membership.CreateUser(ConfigurationManager.AppSettings["AppUserName"], ConfigurationManager.AppSettings["AppPassword"], "mlerwick@yahoo.com");
            }

            if (!Roles.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin");
            }

            if (!Roles.RoleExists("User"))
            {
                Roles.CreateRole("User");
            }

            if (!Roles.IsUserInRole("Admin", "Admin"))
            {
                Roles.AddUserToRole("Admin", "Admin");
            }
        }
    }
}