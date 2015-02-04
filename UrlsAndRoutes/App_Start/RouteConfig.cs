using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            #region For attribute based routing

            /* This is just here for attribute routing purposes */

            routes.MapMvcAttributeRoutes(); // enabling attribute-based routing

            routes.MapRoute(
                "",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "URLsAndRoutes.Controllers" });

            #endregion For attribute based routing


            /* Each lesson is contained in a region. Read from bottom to top. This shows
             * convention-based routing as opposed to attribute routing */

            #region Constraining Routes

            /* We can also constrain rules by passing in an annoynmous type to Routes.MapRoute() with the
             * constraints. Below are examples of constraining routes. The default values are applied first
             * before the constraints are checked. */

            // REGEX
            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new { controller = "^H.*" }, // using regex (controller must start with "H")
            //    new[] { "URLsAndRoutes.Controllers" });

            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}/id/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new { controller = "^H.*", action = "^Index$|^About$" }, // controller must start with "H" and action has to be "Index" or "About"
            //    new [] { "URLsAndRoutes.Controllers" });

            // HTTP METHOD (unrelated to restricting action methods via attributes, happens later in pipeline)
            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}/id/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new { controller = "^H.*", action = "^Index$|^About$", httpMethod = new HttpMethodConstraint("GET", "POST") }, // only GET and POST requests. It doesn't matter what you call the variable (httpMethod)
            //    new[] { "URLsAndRoutes.Controllers" });

            // TYPE AND VALUE

            /* There are a number of built in constraints which can be found at System.Web.Mvc.Routing.Constraints,
             * I am using one which checks for the range of the "id" variable that it is between 10 and 20 */

            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}/id/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new {
            //        controller = "^H.*",
            //        action = "^Index$|^About$",
            //        httpMethod = new HttpMethodConstraint("GET", "POST"),
            //        id = new RangeRouteConstraint(10, 20) // Built in constraint - System.Web.Mvc.Routing.Constraints
            //    },
            //    new[] { "URLsAndRoutes.Controllers" });

            //// combining constraints using CompoundRouteConstraint
            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}/id/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new
            //    {
            //        controller = "^H.*",
            //        action = "^Index$|^About$",
            //        httpMethod = new HttpMethodConstraint("GET", "POST"),
            //        id = new CompoundRouteConstraint(new IRouteConstraint[]
            //        {
            //            new AlphaRouteConstraint(), // id must alphabetic
            //            new MinLengthRouteConstraint(6) // id must have minimum length of 6
            //        })
            //    },
            //    new[] { "URLsAndRoutes.Controllers" });

            // CUSTOM

            /* You can create your own custom constraints by creating a class that is an IRouteConstraint.
             * Here we only match the route with the url if the user agent string contains Chrome. The url
             * segments can be anything as we are just using a catchall. */

            //routes.MapRoute(
            //    "",
            //    "{*catchall}",
            //    new { controller = "Home", action = "Index" },
            //    new { customConstraint = new UserAgentConstraint("Chrome") },
            //    new[] { "URLsAndRoutes.AdditionalControllers" });

            #endregion Constraining Routes

            #region Prioritizing controllers by namespace

            /* When the MVC framework searches for the relevant controller, sometimes it might come across
             * two with the same name (but in different namespaces). This will throw an error. The way to
             * sort this out is to set what namespace(s) you want the MVC framework to give preference to.
             * You do this by adding an array of namespaces as the 4th parameter to RouteCollection.MapRoute().
             * All of the namespaces in the array have the preference. */

            //Route myRoute = routes.MapRoute(
            //    "",
            //    "Home/{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new[] { "UrlsAndRoutes.AdditionalControllers" }); // will search here first

            //// stops it from searching elsewhere if not found in the specified namespace. Passes this setting along to the controller factory
            //myRoute.DataTokens["UseNamespaceFallback"] = false;

            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new[] { "UrlsAndRoutes.Controllers" });

            #endregion Prioritizing controllers by namespace

            #region Variable length routes

            /* You can define support for variable length urls by using a "catchall" variable. You implement
             * this by prefixing a segment in the url pattern with an *. You can give the segment variable
             * any name you like (I choose catchall). Below will pass any segments after the first 3 to the
             * catchall variable in the form segment/segment/segment. It is up to us to seperate out the values
             * in our controllers. */

            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            #endregion Variable length routes

            #region Optional URL segments

            /* Below shows an example of using optional url segments. We specified that the 'id' segment
             * is optional which means if we have a url that has 2 or less segments the 'id' value will
             * not be included (will be null). If we have a url with 3 segments, 'id' will be equal to the
             * third segment. A url with more than 3 segments will not match this route. */

            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}/{id}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            #endregion Optional URL segments

            #region Custom segment variables

            /* We can also define our own segment variables as long as we don't use the names controller,
             * action or area which are reserved. Here we have created a route which matches urls with
             * 0-3 segments. Look at Controllers.HomeController.CustomVariable() to see how we access
             * custom variables. */

            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}/{id}",
            //    new { controller = "Home", action = "Index", id = "DefaultId" });

            #endregion Custom segment variables

            #region Static URL segments and ordering

            /* Not all segments need to be variables. The below examples of static segments allow me to use
             * the following urls: http://mydomain.com/Public/Home/Index and http://mydomain.com/XHome/Index.
             * NOTE: The order of routes are important, if the last route came first and I went to
             * http://mydomain.com/XHome/Index, I would get a 404 as the url matches the route but there is
             * no controller called 'XHome'. When mapping routes we order it by specifity - most general last */

            // matches urls with 1-2 segments, the first must begin with 'X'
            //routes.MapRoute(
            //    "",
            //    "X{controller}/{action}",
            //    new { controller = "Home", action = "Index" });

            // matches urls with 1-3 segments, the first must be 'Public'
            //routes.MapRoute(
            //    "",
            //    "Public/{controller}/{action}",
            //    new { controller = "Home", action = "Index" });

            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}",
            //    new { controller = "Home", acton = "Index" });

            // we can create an alias for old controllers or actions also by doing the following
            //routes.MapRoute(
            //    "",
            //    "Shop/{action}",
            //    new { controller = "Home" });

            //routes.MapRoute(
            //    "",
            //    "Shop/OldAction",
            //    new { controller = "Home", action = "Index" });

            #endregion Static URL segments and ordering

            #region Defining default values

            /* The third parameter for RouteCollection.MapRoutes() allows you to specify default values for segments
             * when they are empty. Here we are allowing a url to match the route if it has 0 - 2 segments. If the url
             * has 2 segments it will work as usual, the first segment being used as the controller name and the second
             * as the action name. If we only get one segment passed in, it will be used as the controller name and the
             * action will be 'Index' as that is what is specified in the default values. If we get no segments passed
             * in, the controller will be 'Home' and the action will be 'Index' */

            //routes.MapRoute(
            //    "",
            //    "{controller}/{action}",
            //    new { controller = "Home", acton = "Index" });

            #endregion Defining default values

            #region Create route

            /* Below is an example of creating a new route using a URL pattern as a constructor parameter.
             * Then we add it to the RouteCollection object passing an optional name. We need to also pass
             * in a new MvcRouteHandler */

            //Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            //routes.Add("MyRoute", myRoute);

            /* Below is a more convenient way of adding routes. Using the RouteCollection.MapRoutes() method
             * I do not need to pass in a MvcRouteHandler as this is done for me behind the scenes. NOTE: This
             * can only be used with MVC */

            //routes.MapRoute("", "{controller}/{action}");

            #endregion Create route
        }
    }
}
