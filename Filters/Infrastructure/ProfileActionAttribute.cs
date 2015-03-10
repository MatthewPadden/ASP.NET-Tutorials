using System.Diagnostics;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    /* With Action Filters you can perform a task which spans the execution of the action method.
     * Here we are doing this my timing how long it takes for an Action Method to complete. We
     * have a Stopwatch object which lasts the execution of the Action Method. */

    public class ProfileActionAttribute : FilterAttribute, IActionFilter
    {
        private Stopwatch timer;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        /* OnActionExecuted will still be called even if the request was cancelled by OnActionExecuting.
         * It can be used to free up resources. To check if it is cancelled look at the
         * filterContext.Canceled property */

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            timer.Stop();

            /* Text will appear before the result of the Action because this is executed after the
             * Action method completes but before the result is processed */

            if (filterContext.Exception == null)
                filterContext.HttpContext.Response.Write(
                    string.Format("<div>Action method elapsed time: {0:F6}</div>", timer.Elapsed.TotalSeconds));
        }
    }
}