using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                "save",
                "{controller}/{action}/{ip}/{port}/{rate}/{recordTime}/{fileName}",
                 defaults: new { controller = "Main", action = "save", ip = "127.0.0.1", port = 5400, rate = 4,
                     recordTime = 10, fileName = "flight1" }
            );

            routes.MapRoute(
                "name",
                "{controller}/{action}/{ip}/{port}/{rate}",
                 new { controller = "Main", action = "display", ip = "127.0.0.1", port = 5400, rate = 4}
            );
                
            routes.MapRoute(
                "load",
                "{controller}/{action}/{fileName}/{rate}",
                 new { controller = "Main", action = "load", fileName = "flight1" ,rate = 4}
            );

        }
    }
}
