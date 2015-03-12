using System.Web.Mvc;
using System.Web.Routing;
using ControllerExtensibility.Infrastructure;

namespace ControllerExtensibility
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            /* Here we register our custom controller factory using the ControllerBuilder
             * object. Once registered it will take over the handling of all the requests
             * the application receives */

            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());
        }
    }
}