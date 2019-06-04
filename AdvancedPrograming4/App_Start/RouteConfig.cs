using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdvancedPrograming4
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("display", "{controller}/{action}/{ip_or_file}/{port_or_refreshRate}/{refreshRate}",
            defaults: new
            {
                controller = "First",
                action = "display",
                ip_or_file = "127.0.0.1",
                port_or_refreshRate = 5400,
                refreshRate = UrlParameter.Optional
            });

            routes.MapRoute("save","{controller}/{action}/{ip}/{port}/{refreshRate}/{timeout}/{fileName}",
            defaults: new
            {
                controller = "First",
                action = "display",
                ip = "127.0.0.1",
                port = 5400 ,
                refreshRate = 4,
                timeout = 10,
                fileName = "flight"
            });

            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "First", action = "display", id = UrlParameter.Optional }
            );
        }
    }
}
