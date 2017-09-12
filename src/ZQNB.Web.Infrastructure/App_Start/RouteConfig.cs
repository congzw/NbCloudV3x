using System.Web.Mvc;
using System.Web.Routing;
using ZQNB.Common;

namespace ZQNB.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var defaultProjectPrefix = NbRegistry.Instance.CurrentProjectPrefix;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "",
                "",
                new { controller = "Home", action = "Index"},
                new[] { string.Format("{0}.Web.Controllers", defaultProjectPrefix) }
            );

            routes.MapRoute(
                "Common_Default",
                "Common/{controller}/{action}",
                defaults: new {controller = "Home", action = "Index"},
                namespaces: new[] {string.Format("{0}.Web.Controllers", defaultProjectPrefix)}
                ); //.RouteHandler = new DashRouteHandler();
        }
    }
}
