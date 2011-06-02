using System.Web.Mvc;
using System.Web.Routing;
using Uranium.Core.Infrastructure;

namespace Uranium.Mvc.Infrastructure
{
    public class RoutesInitialization : IBootstrapMember
    {
        public void Execute()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}
