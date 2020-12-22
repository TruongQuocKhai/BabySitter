using System.Web.Mvc;

namespace BabySitter.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { controller = "Admin", action = "Login", id = UrlParameter.Optional }
            );
            context.MapRoute(
              name: "",
              url: "admin",
              defaults: new { controller = "Admin", action = "Login", id = UrlParameter.Optional },
               namespaces: new[] { "BabySitter.Controller" }

          );
        }
    }
}