using System;
using System.Web.Mvc;
using ControllersAndActions.Infrastructure;

namespace ControllersAndActions.Controllers
{
    /* This controller derives from System.Web.Mvc.Controller which itself is an IController. Instead of
     * having a single Execute() method, the Controller base class implements the Execute() method and takes
     * responsibilty for invoking the action method whose name matches the action value in the route data.
     * This is also our link to the Razor view system.
     * Our job when using a derivation of Controller is to implement action methods, obtain whatever input
     * we need to process a request and provide a suitable response.
     *
     * The three key features of the Controller class are:
     * 1. Action methods
     * 2. Action results
     * 3. Filters */

    public class DerivedController : Controller
    {
        // This is an action method as it ran when the routing system matches a URL
        // It is also returns an Action result which in this case says to render a view.
        public ActionResult Index()
        {
            ViewBag.Message = "Hello from the DerivedController Index method";
            return View("MyView");
        }

        /* This is an example of getting input data from Context objects. The Controller base class gives
         * us access to a set of convenience properties to access information about the request. These include
         * Request, Response, RouteData, HttpContext and Server. These are all held on the
         * Controller.ControllerContext property */

        public ActionResult ShowWeatherForecast()
        {
            string city = (string)RouteData.Values["city"];
            DateTime forDate = DateTime.Parse(Request.Form["forDate"]);
            // ... implement weather forecast here ...
            return View();
        }

        /* This is a neater way to recieve incoming data. The MVC Framework will provide values for our
         * parameters by checking the context objects on our behalf, including Request.QueryString,
         * Request.Form and RouteData.Values. The names of our parameters are treated case-insensitively.
         *
         * If the MVC Framework cannot find a value for a reference type parameter, the action method will
         * still be called and null will be passed for that reference type. If it cannot find a value for
         * a value type, an exception will be thrown and the action method will not be called (get around
         * this by using nullabe 'int?' or providing default values). */

        public ActionResult ShowWeatherForecast(string city, DateTime forDate)
        {
            //... implement weather forecast here ...
            return View();
        }

        /* Here is an example of using optional parameters. If query (reference type) is not provided the
         * default value is 'all', if page (value type) is not provided then it uses its default value of 1
         * If a request does contain a value for a parameter but it cannot be converted to the correct type
         * then the framework will pass the default value for that parameter type and will register the attempted
         * value as a validation error in the context object; ModelState and the request is handled as normal. */

        public ActionResult Search(string query = "all", int page = 1)
        {
            // ... process request ...
            return View();
        }

        /* Problems with the method below are that the controller must contain details of HTML or URL structure,
         * it is hard to unit test as we would have to mock the Response object and processing the output we
         * recieve from the controller which is messy. It is also very error-prone when handling fine details of
         * the response. */

        public void ProduceOutput()
        {
            if (Server.MachineName == "TINY")
                Response.Redirect("/Basic/Index");
            else
                Response.Write("Controller: Derived, Action: ProduceOutput");
        }

        /* To address the issues above MVC has provided us with ActionResults which are used to seperate
         * stating our intentions from executing our intensions. Instead of working directly with the Response
         * object, we return an object derived from ActionResult that describes what we want to do.
         * When the MVC Framework recieves an ActionResult object from the action method, it calls the
         * ExecuteResult() method of that object which deals with the Response object. Here is an example using
         * a custom ActionResult in Infrastructure */

        public ActionResult ProduceOutputWithCustomActionResult()
        {
            if (Server.MachineName == "TINY")
                return new CustomRedirectResult { Url = "/Basic/Index" };
            else
                Response.Write("Controller: Derived, Action: ProduceOutput");
            return null;
        }

        /* Here we are using one of the built-in ActionResults. This will redirect us to "/Basic/Index" */

        public ActionResult ProduceOutputWithActionResult()
        {
            return new RedirectResult("/Basic/Index");
        }

        /* Most of the ActionResults have helpers in the Controller base class. Here we are returning a
         * "RedirectResult" by using the helper "Redirect()" on the Controller base class. */

        public ActionResult ProduceOutputWithActionResultHelper()
        {
            return Redirect("/Basic/Index");
        }
    }
}