using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using ControllerExtensibility.Controllers;

namespace ControllerExtensibility.Infrastructure
{
    /* As a request arrives it hits the Routing framework first. After that it comes into the Controller
     * Factory for the application. This is a Custom Controller Factory which we are going to use to
     * demonstrate the role of them. */

    public class CustomControllerFactory : IControllerFactory
    {
        /* CreateController is the most imprtant part of a controller factory as MVC calls it when it
         * needs a controller to service a request. The RequestContext object which gets passed gives
         * us RouteData and HttpContext. The controllerName string is the controller value from the
         * routed url. The only rule we must follow for CreateController is that we return an
         * IController. */

        /* Most of the convention in MVC are there because of how the default Controller Factory works.
         * I am implementing one of these conventions in the CreateController method by appending
         * 'Controller' to the class name so that a request for Product leads to the ProductController
         * class being instantiated. */

        /* Two important points for a controller factory:
         *  1. Has sole responsibility for matching request to controllers
         *  2. Can change the request to alter the behaviour of subsequent steps in the request
         *     processing pipeline. */

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            /* This is very dumbed down as I have hard-coded the controller class names so I can
             * instantiate them directly as opposed to dynamically searching for the correct
             * controller. */

            Type targetType = null;

            switch (controllerName)
            {
                case "Product":
                    targetType = typeof(ProductController);
                    break;

                case "Customer":
                    targetType = typeof(CustomerController);
                    break;

                default:
                    requestContext.RouteData.Values["controller"] = "Product"; // see point 2
                    targetType = typeof(ProductController);
                    break;
            }

            // good practice to use DependencyResolver to instantiate controller classes
            return targetType == null ? null : (IController)DependencyResolver.Current.GetService(targetType);
        }

        /* GetControllerSessionBehavior is used by the MVC framework to determine if session data
         * should be maintained for a controller. */

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        /* ReleaseController is called when a controller object created by the CreateController
         * method is no longer needed. */

        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}