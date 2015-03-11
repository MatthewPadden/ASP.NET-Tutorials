using System.Web.Mvc;
using Filters.Infrastructure;

namespace Filters.Controllers
{
    [SimpleMessage(Message = "A")]
    public class CustomerController : Controller
    {
        /* Here we have an action method which has two of the same action filters
         * applied to it. MVC will run these at any order if we di not specify an
         * order to run them in. Here we specify the order. The default order is -1.
         * A note is that the MVC framework stacks filters so the result will be
         * the following:
         * 
         *  [Before Action: A]  OnActionExecuting   - A
         *  [Before Action: B]  OnActionExecuting   - A
         *  [After Action: B]   OnActionExecuted    - B
         *  [After Action: A]   OnActionExecuted    - B
         *  
         * If filters are given the same order then MVC will run them based on their
         * location. Global filters are run first, then filters on the class, then
         * filters on the action methods. This order is reversed in the case of
         * Exception filters.
         *
         *  [SimpleMessage(Message = "A", Order = 1)]
         *  [SimpleMessage(Message = "B", Order = 2)]
         *  public string Index()
         *  {
         *      return "This is the Customer controller";
         *  }
         */

        /* I have applied the SimpleMessage action filter to the class and the action
         * method. In this case I do not want the class level filter to run, I want only
         * the action method filter to run. To override a filter we use a filter override
         * which tells MVC to ignore any filters that have been defined at a higher level.
         * I have applied my custom override filter CustomOverrideActionFilters which targets
         * action filters. */

        [CustomOverrideActionFilters]
        [SimpleMessage(Message = "B")]
        public string Index()
        {
            return "This is the Customer controller";
        }
    }
}