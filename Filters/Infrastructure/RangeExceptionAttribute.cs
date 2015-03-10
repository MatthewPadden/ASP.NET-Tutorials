using System;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    /* Exception filters are only run if an unhandled exception has been thrown when invoking an action
     * method. This can occur from the following locations:
     *  1. Another kind of filter
     *  2. The action method itself
     *  3. When the action result is executed */

    /* Exception filters must implement IExceptionFilter. In order for a .NET class to be treated as
     * an MVC filter, it must implement IMvcFilter which we get from FilterAttribute and which provides
     * some useful basic features such as handling the default order in which filters are processed */

    public class RangeExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        /* OnException is called when an unhandled exception arises */

        public void OnException(ExceptionContext filterContext)
        {
            // check if the exception is already handled before checking the type of exception
            if (!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException)
            {
                //filterContext.Result = new RedirectResult("~/Content/RangeErrorPage.html");

                // instead of going to a static page we go to a view which can give contextual information
                int val = (int)(((ArgumentOutOfRangeException)filterContext.Exception).ActualValue);
                filterContext.Result = new ViewResult
                {
                    ViewName = "RangeError",
                    ViewData = new ViewDataDictionary<int>(val)
                };

                // set the exception to handled to prevent the exception being raised again
                filterContext.ExceptionHandled = true;
            }
        }
    }
}