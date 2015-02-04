using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        #region Custom segment variables

        #region From RouteData.Values

        /* To get access to custom segment variable from an action method we look in RouteData.Values*/

        //public ActionResult CustomVariable()
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    ViewBag.CustomVariable = RouteData.Values["id"];
        //    return View();
        //}

        #endregion From RouteData.Values

        #region From action method parameters

        /* If I define parameters to the action method with names that match the url pattern variables,
         * the MVC Framework will pass the values obtained from the URL as parameters to the action method.
         * It does this by using Model Binding, it will try and conver the url variable to whatever type the
         * parameter is on my action method. This is a more elegant. */

        //public ActionResult CustomVariable(string id = "DefaultId") // setting default value here instead of on the route
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    //ViewBag.CustomVariable = id ?? "<no value>"; // checking for an optional segment variable if default value not set
        //    ViewBag.CustomVariable = id;
        //    return View();
        //}

        #endregion From action method parameters

        #region Using catchall variables

        public ActionResult CustomVariable(string id = "DefaultId")
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomVariable";
            ViewBag.CustomVariable = id;
            ViewBag.Catchall = RouteData.Values["catchall"]; // form: segment/segment/segment
            return View();
        }

        #endregion Using catchall variables

        #endregion Custom segment variables
    }
}