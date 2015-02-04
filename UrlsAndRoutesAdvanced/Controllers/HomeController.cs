using System.Web.Mvc;

namespace UrlsAndRoutesAdvanced.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        public ActionResult CustomVariable(string id = "DefaultId")
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomVariable";
            ViewBag.CustomVariable = id;
            return View();
        }

        public ViewResult MyActionMethod()
        {
            string myActionUrl = Url.Action("Index", new { id = "MyID" }); // /Home/Index/MyID
            string myRouteUrl = Url.RouteUrl(new { contoller = "Home", action = "Index" }); // /

            //... do something with the URLs...
            return View();
        }

        //public RedirectToRouteResult MyActionMethod()
        //{
        //    /* Two methods of doing the same thing */

        //    //return RedirectToAction("Index");

        //    return RedirectToRoute(new
        //    {
        //        controller = "Home",
        //        action = "Index",
        //        id = "MyID"
        //    });
        //}
    }
}