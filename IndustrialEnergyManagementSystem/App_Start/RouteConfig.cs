using System.Web.Mvc;
using System.Web.Routing;

namespace IndustrialEnergyManagementSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignore certain resource requests
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Default route: Root URL "/" will open MachineController → Index
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Machine", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}