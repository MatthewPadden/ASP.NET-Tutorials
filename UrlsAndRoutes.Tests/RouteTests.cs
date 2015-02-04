using System;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UrlsAndRoutes.Tests
{
    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void TestIncomingRoutes()
        {
            Helpers.TestRouteMatch("~/", "Home", "Index");
            //Helpers.TestRouteMatch("~/Home", "Home", "Index");
            //Helpers.TestRouteMatch("~/Home/Index", "Home", "Index");

            //Helpers.TestRouteMatch("~/Home/About", "Home", "About");
            //Helpers.TestRouteMatch("~/Home/About/MyId", "Home", "About", new { id = "MyId" });
            //Helpers.TestRouteMatch("~/Home/About/MyId/More/Segments", "Home", "About", new { id = "MyId", catchall = "More/Segments" });

            //Helpers.TestRouteFail("~/Home/OtherAction");
            //Helpers.TestRouteFail("~/Account/Index");
            //Helpers.TestRouteFail("~/Account/About");
        }

        public static class Helpers
        {
            public static void TestRouteMatch(string url, string controller, string action, object routeProperties = null, string httpMethod = "GET")
            {
                // Arrange
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);

                // Act
                RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
            }

            public static void TestRouteFail(string url)
            {
                // Arrange
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);

                // Act - process the route
                RouteData result = routes.GetRouteData(CreateHttpContext(url));

                // Assert
                Assert.IsTrue(result == null || result.Route == null);
            }

            private static HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
            {
                // create the mock request
                Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
                mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
                mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

                // create the mock response
                Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
                mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

                // create the mock context, using the request and response
                Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
                mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
                mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

                // return the mocked object
                return mockContext.Object;
            }

            private static bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null)
            {
                Func<object, object, bool> valCompare = (v1, v2) =>
                {
                    return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
                };

                bool result = valCompare(routeResult.Values["controller"], controller) && valCompare(routeResult.Values["action"], action);

                if (propertySet != null)
                {
                    PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                    foreach (PropertyInfo pi in propInfo)
                    {
                        if (!(routeResult.Values.ContainsKey(pi.Name)
                            && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null))))
                        {
                            result = false;
                            break;
                        }
                    }
                }

                return result;
            }
        }
    }
}