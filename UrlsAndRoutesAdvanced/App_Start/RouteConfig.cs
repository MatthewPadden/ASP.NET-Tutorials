using System.Web.Mvc;
using System.Web.Routing;
using UrlsAndRoutesAdvanced.Infrastructure;

namespace UrlsAndRoutesAdvanced
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            /* This shows bypassing the routing system */
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/{filename}.html");

            routes.Add(new Route("SayHello", new CustomRouteHandler()));

            routes.Add(new LegacyRoute(
                "~/articles/Windows_3.1_Overview.html",
                "~/old/.NET_1.0_Class_Library"));

            /* Below shows a route which is more specific than "MyRoute" if the routing system is generating
             * an outgoing URL. It will look at this collection of routes and pick the first matching route. 
             * If we supply an action from Home controller it will match below, however if the controller is
             * not "Home" it wil not match as the default for controller is "Home" */
            //routes.MapRoute(
            //    null,
            //    "App/Do{action}",
            //    new { controller = "Home" });

            routes.MapRoute(
                null,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
