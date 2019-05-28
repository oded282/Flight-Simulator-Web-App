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

/*
            routes.MapRoute(
                "2",
                "{controller}/{action}/{ip}/{port}",
                new { controller = "Main", action = "display", ip ="127.0.0.1" , port = 5400}
            );
            */
            routes.MapRoute(
                "name",
                "{controller}/{action}/{ip}/{port}/{rate}",
                 new { controller = "Main", action = "display", ip = "127.0.0.1", port = 5400, rate = 4}
            );

            
        }
    }
}
