using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class CustomActionAttribute : FilterAttribute, IActionFilter
    {
        /* OnActionExecuting gets run before the ActionMethod */

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsLocal)
                filterContext.Result = new HttpNotFoundResult();
        }

        /* OnActionExecuted get ran after the ActionMethod */

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // You do not need to implement each of these methods
        }
    }
}