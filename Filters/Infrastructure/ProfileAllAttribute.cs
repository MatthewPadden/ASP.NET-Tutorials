using System.Diagnostics;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    /* Here we are using the built in ActionFilterAttribute which is basically an
     * Action Filter and a Result Filter all in one. It implements both IActionFilter
     * and IResultFilter. We do not need to override all the methods, just the ones we
     * need as below. */

    public class ProfileAllAttribute : ActionFilterAttribute
    {
        private Stopwatch timer;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();

            filterContext.HttpContext.Response.Write(
                    string.Format("<div>Total elapsed time: {0:F6}</div>", timer.Elapsed.TotalSeconds));
        }
    }
}