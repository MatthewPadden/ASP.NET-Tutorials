using System.Web.Mvc;
using Filters.Infrastructure;

namespace Filters
{
    public class FilterConfig
    {
        /* Here we can register Global Filters which will run across all controllers'
         * action methods. */

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ProfileAllAttribute());
        }
    }
}