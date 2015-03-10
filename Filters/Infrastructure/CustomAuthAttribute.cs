using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    /* This custom filter is a Authorization filter as it inherits from AuthorizeAttribute which
     * implements IAuthorizationFilter. We could have our own implemntation of IAuthorizationFilter
     * but that would mean we would have to write our own security code which is dangerous. Here
     * we use MVC's own implementation and add in our own custom code which lets us use MVC's security
     * code. */

    /* Types of filters and order they are ran:
     * 1. Authentication    -   IAuthenticationFilter   -   Before action method, can be run again
     * 2. Authorization     -   IAuthorizationFilter    -   Before action method, can be run again
     * 3. Action            -   IActionFilter           -   Before and after action method
     * 4. Result            -   IResultFilter           -   Before and after the action result is executed
     * 5. Exception         -   IExceptionHandler       -   Only if another filter, the action method or
     *                                                      the action result throws an exception */

    /// <summary>
    /// Does not allow access if the request is local
    /// </summary>
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        private bool localAllowed;

        public CustomAuthAttribute(bool allowedParam)
        {
            localAllowed = allowedParam;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsLocal)
                return localAllowed;
            else
                return true;
        }
    }
}