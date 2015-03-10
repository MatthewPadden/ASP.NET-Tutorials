using System;
using System.Web.Mvc;
using Filters.Infrastructure;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        /* In-built Authorization filter. It has two public properties Users and Roles with are
         * string comma-separated lists of the users and roles in the system. When we use the
         * AuthorizeAttribute we can supply users and/or roles to check against. */

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
        public string RangeTestWithBHandleError(int id)
        {
            if (id > 100)
                return string.Format("The id value is: {0}", id);
            else
                throw new ArgumentOutOfRangeException("id", id, "");
        }

        [ProfileAction]
        [ProfileResult]
        [ProfileAll]
        public string FilterTest()
        {
            return "This is the FilterTest action";
        }
    }
}