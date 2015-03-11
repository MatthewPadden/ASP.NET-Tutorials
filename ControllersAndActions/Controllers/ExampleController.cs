using System;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        /* When we do not specify what view we want, the MVC Framework looks for a View with
         * the same name as the Action method. This is actually determined by looking at
         * RouteData.Values["action"] not by looking at the name of the action method which
         * is calling */

        public ViewResult Index()
        {
            return View();
            //return View("Index"); // secifying a view
            //return View("Index", "_AlternativeLayoutPage"); // specifying an alternative layout page
            //return View("~/Views/Other/Index.chtml"); // skipping the search step
        }

        public ViewResult IndexWithViewModel()
        {
            DateTime date = DateTime.Now;
            return View(date);
        }

        /* Below is just for testing purposes */

        public ViewResult IndexToTestViewModel()
        {
            return View((object)"Hello World!");
        }

        public ViewResult IndexWithViewBag()
        {
            ViewBag.Message = "Hello";
            ViewBag.Date = DateTime.Now;

            return View();
        }

        /* Using the Redirect helper on Controller to perform a 302 to a literal URL */

        public RedirectResult Redirect()
        {
            return Redirect("/Example/Index");
        }

        /* Using the Redirect helper on Controller to perform a 301 to a literal URL */

        public RedirectResult RedirectPermanent()
        {
            return RedirectPermanent("/Example/Index");
        }

        /* Using the RedirectToRoute helper on Controller to perform a 302 to a route */

        public RedirectToRouteResult RedirectToRoute()
        {
            return RedirectToRoute(new
                {
                    controller = "Example",
                    action = "Index",
                    ID = "MyID"
                });
        }

        /* RedirectToAction is just a wrapper around RedirectToRoute. It is another helper on Controller
         * and can be overloaded to include controller. Use RedirectToActionPermanent for 301 */

        public RedirectToRouteResult RedirectToAction()
        {
            return RedirectToAction("Index");

            // return RedirectToAction("Index", "Example");
        }

        /* If you want to preserve data across a redirection, you can use TempData. This is similar to
         * Session data but once it is read it is marked for deletion. */

        public RedirectToRouteResult RedirectToActionWithTempData()
        {
            TempData["Message"] = "Hello";
            TempData["Date"] = DateTime.Now;
            return RedirectToAction("TempData");
        }

        public ViewResult TempDataResult()
        {
            ViewBag.Message = TempData["Message"]; // Deleted once read
            ViewBag.Date = TempData["Date"]; // Deleted once read

            DateTime time = (DateTime)TempData.Peek("Date"); // Lets you read without marking it for deletion

            TempData.Keep("Date"); // Keeps key until read again

            return View();
        }

        /* Returning status codes, no helper method for HttpStatusCodeResult */

        public HttpStatusCodeResult StatusCode()
        {
            return new HttpStatusCodeResult(404, "URL cannot be serviced");
        }

        /* This is a more convenient version of a 404 using HttpNotFound from Controller */

        public HttpStatusCodeResult StatusCodeConenient()
        {
            return HttpNotFound();
        }

        /* Below is implemented by HttpUnauthorizedResult which is a wrapper around HttpStatusCode */

        public HttpStatusCodeResult NotAuthorisedStatusCode()
        {
            return new HttpUnauthorizedResult();
        }
    }
}