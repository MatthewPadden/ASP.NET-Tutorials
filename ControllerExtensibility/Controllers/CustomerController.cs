using System.Web.Mvc;
using ControllerExtensibility.Models;

namespace ControllerExtensibility.Controllers
{
    public class CustomerController : Controller
    {
        public ViewResult Index()
        {
            return View("Result", new Result { ControllerName = "Customer", ActionName = "Index" });
        }

        public ViewResult List()
        {
            return View("Result", new Result { ControllerName = "Customer", ActionName = "List" });
        }
    }
}