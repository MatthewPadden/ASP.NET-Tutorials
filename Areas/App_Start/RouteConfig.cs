using System.Web.Mvc;
using System.Web.Routing;

namespace Areas
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Areas.Controllers" } // Needed so we specify to look in Areas.Controllers before Areas.Areas.Admin.Controllers
            );
        }
    }
}