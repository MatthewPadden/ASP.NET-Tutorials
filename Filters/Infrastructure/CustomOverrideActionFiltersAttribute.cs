using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Filters.Infrastructure
{
    /* This is an Override Filter. We use this when we want to override a filter at a higher level
     * than the one we want to run. */

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomOverrideActionFiltersAttribute : FilterAttribute, IOverrideFilter
    {
        // return the type of filter we want to override
        public Type FiltersToOverride
        {
            get { return typeof(IActionFilter); }
        }
    }
}