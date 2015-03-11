using System;
using System.Diagnostics;
using System.Web.Mvc;
using Filters.Infrastructure;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        /* In-built Authorization filter. It has two public properties Users and Roles with are
         * string comma-separated lists of the users and roles in the system. When we use the
         * AuthorizeAttribute we can supply users and/or roles to check against. */

        /* The Controller class implements IAuthenticationFilter, IAuthorizationFilter, IActionFilter,
         * IResultFilter and IExceptionFilter interfaces. It also provides empty virtual implementations
         * of the relevant OnXXX methods for each filter type. This means we can override the
         * filter methods in the controller using filters with attributes. We do this for FilterTest().
         * This is */

        private Stopwatch timer;

        [Authorize(Users = "admin")]
        //[CustomAuth(true)] // when set to false, we will be given the login page
        public string Index()
        {
            return "This is the Index action of the Home controller";
        }

        /* You can combine Authentication and Authorization filters to make the access to an
         * Action method even more granular */

        [GoogleAuth]
        [Authorize(Users = "bob@google.com")]
        public string List()
        {
            return "This is the List action on the Home controller";
        }

        /* Without adding an Exception filter to this action method, if a user gets here with the
         * id segment variable < 100, we will get the yellow creen of death. By adding an Exception
         * filter we can handle the exception ourselves and avoid that. */

        [RangeException]
        public string RangeTest(int id)
        {
            if (id > 100)
                return string.Format("The id value is: {0}", id);
            else
                throw new ArgumentOutOfRangeException("id", id, "");
        }

        /* This uses the built in Exception filter called HandleError. As we can see, we tell it which
         * exception to look out for and then what view to return, we can also specify the layout page
         * to use. The model that will be passed to the view will be a HandleErrorInfo object which is
         * a wrapper around the exception. */

        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "RangeErrorWithHandleError")]
        public string RangeTestWithHandleError(int id)
        {
            if (id > 100)
                return string.Format("The id value is: {0}", id);
            else
                throw new ArgumentOutOfRangeException("id", id, "");
        }

        /* Filter is applied without using attributes. Look below method */

        public string FilterTest()
        {
            return "This is the FilterTest action";
        }

        #region Filters without attributes
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();

            filterContext.HttpContext.Response.Write(
                string.Format("<div>Total elapsed time: {0}</div>", timer.Elapsed.TotalSeconds));
        }
        #endregion Filters without attributes
    }
}