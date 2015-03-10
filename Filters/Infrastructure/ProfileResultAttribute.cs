using System.Diagnostics;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class ProfileResultAttribute : FilterAttribute, IResultFilter
    {
        private Stopwatch timer;

        /* OnResultExecuting gets invoked after the ActionMethod has returned an
         * Action Result but before the Action Result is executed */

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        /* OnResultExecuted get invoked after the Action Result gets executed */

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();

            filterContext.HttpContext.Response.Write(
                string.Format("<div>Result elapsed time: {0:F6}</div>", timer.Elapsed.TotalSeconds));
        }
    }
}