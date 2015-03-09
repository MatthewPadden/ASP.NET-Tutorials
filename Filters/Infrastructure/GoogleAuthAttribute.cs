using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace Filters.Infrastructure
{
    /* This is an AuthenticationFilter as it implements the IAuthenticationFilter interface.
     * Authentication filters run before any other filters and can also be combined with authorization
     * filters to provide authentication challenges for requests that don't comply to the authorization
     * policy.
     * Authentication filters also run after an action method has been executed but before the ActionResult
     * is processed.
     *
     * Authentication filter -> <other filters> -> ActionMethod -> Authentication filter -> ActionResult */

    public class GoogleAuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        /* OnAuthentication gets called by the controller before any other filter. IF OnAuthentication sets
         * a value for AuthenticationContext.Result then OnAuthenticationChallenge is called. */

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            IIdentity ident = filterContext.Principal.Identity;

            // check we are using a google account
            if (!ident.IsAuthenticated || !ident.Name.EndsWith("@google.com"))
                filterContext.Result = new HttpUnauthorizedResult();
        }

        /* OnAuthenticationChallenge is called by the MVC framework whenever a request has failed
         * the authentication or authorization policies for an action method. If we do not set the
         * value of Result here, it will use the Result set in OnAuthentication. Also gets called
         * before the execution of the ActionMethod */

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            /* check to see if result is set to avoid challenging user when the filter is run after
             * the action method is executed */
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary // we do not have the convenience methods as this is not a controller
                    {
                        {"controller", "GoogleAccount"},
                        { "action", "Login" },
                        { "returnUrl", filterContext.HttpContext.Request.RawUrl }
                    });
            }
            else
                FormsAuthentication.SignOut(); // So the user only has temporary elevated priveleges
        }
    }
}