using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    /* All routes on the action methods will have a prefix of "Users" e.g "Users/Add/.." */

    [RoutePrefix("Users")]
    public class CustomerController : Controller
    {
        /* Your url pattern to match against, once added it can no longer be accessed through
         * the convention-based routes in RouteConfig.cs file. You can create multiple Route
         * attributes. */

        [Route("~/Test")] // Ignores the RoutePrefix
        public ActionResult Index()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        /* Below shows how we can still use segment varibles and constraints in attribute-based
         * routing. The segment variables work as before via modelbinding. I have put a constraint
         * on the 'id' segment. It says that it must be a value which can be converted to a valid
         * int. It corresponds to the IntRouteConstraint class. */

        [Route("Add/{user}/{id:int}")]
        public string Create(string user, int id)
        {
            return string.Format("User: {0}, ID: {1}", user, id);
        }

        /* Note the same route as above except for constraint on second segment.
         * ~/Users/Add/matt/1 vs. Users/Add/matt/passwd
         * I have also shown how to combine constraints. The 2nd segment must be alpha with exactly
         * 6 letters. */

        [Route("Add/{user}/{password :alpha:length(6) }")]
        public string ChangePass(string user, string password)
        {
            return string.Format("ChangePass Method - User: {0}, Pass: {1}", user, password);
        }

        public ActionResult List()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "List";
            return View("ActionName");
        }
    }
}