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
    }
}