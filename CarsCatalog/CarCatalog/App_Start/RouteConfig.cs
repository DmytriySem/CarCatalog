using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarCatalog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //     name: "Cars",
            //     url: "Car/{action}",
            //     defaults: new { controller = "Car", action = "Cars", id = UrlParameter.Optional, name = UrlParameter.Optional }
            //     );

            //routes.MapRoute(
            //     name: "Models",
            //     url: "Model/{action}/{id}/{name}",
            //     defaults: new { controller = "Model", action = "Models", id = UrlParameter.Optional, name = UrlParameter.Optional }
            //     );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Catalog", action = "Index" }
                );
        }
    }
}
