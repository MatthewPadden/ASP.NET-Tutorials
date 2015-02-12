using System.Web.Mvc;
using ControllersAndActions.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControllersAndActions.Tests
{
    public class ExampleControllerTests
    {
        [TestClass]
        public class IndexMethod
        {
            [TestMethod]
            public void ReturnsCorrectView()
            {
                // Arrange - create controller
                ExampleController target = new ExampleController();

                // Act - call the action method
                ViewResult result = target.Index();

                // Assert - check the result
                Assert.AreEqual("", result.ViewName);
            }

            [TestMethod]
            public void TestViewModel()
            {
                // Arrange - create controller
                ExampleController target = new ExampleController();

                // Act
                ViewResult result = target.IndexToTestViewModel();

                // Assert
                Assert.AreEqual("Hello World!", (string)result.ViewData.Model);
            }

            [TestMethod]
            public void TestViewBag()
            {
                // Arrange - create controller
                ExampleController target = new ExampleController();

                // Act
                ViewResult result = target.IndexWithViewBag();

                // Assert
                Assert.AreEqual("Hello", result.ViewBag.Message);
            }

            [TestMethod]
            public void RedirectTest()
            {
                // Arrange
                ExampleController target = new ExampleController();

                // Act
                RedirectResult result = target.Redirect();

                // Assert
                Assert.IsFalse(result.Permanent);
                Assert.AreEqual("/Example/Index", result.Url);
            }

            [TestMethod]
            public void RedirectToRouteTest()
            {
                // Arrange
                ExampleController target = new ExampleController();

                // Act
                RedirectToRouteResult result = target.RedirectToRoute();

                // Assert
                Assert.AreEqual("Example", result.RouteValues["controller"]);
                Assert.AreEqual("Index", result.RouteValues["action"]);
                Assert.AreEqual("MyID", result.RouteValues["id"]);

            }

            [TestMethod]
            public void StatusCodeResultTest()
            {
                // Arrange
                ExampleController target = new ExampleController();

                // Act
                HttpStatusCodeResult result = target.NotAuthorisedStatusCode();

                // Arrange
                Assert.AreEqual(401, result.StatusCode);
            }
        }
    }
}