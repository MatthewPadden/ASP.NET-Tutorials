using System.Web.Mvc;
using System.Web.Routing;

namespace Areas
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            /* Causes the MVC framework to go through all the classes in our application, find ones derived
             * from AreaRegistration and then call RegisterArea() on them. It must come before RegisterRoutes()
             * so the order of the routes are correct */
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}