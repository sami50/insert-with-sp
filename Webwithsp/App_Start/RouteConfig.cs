﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Webwithsp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "products", action = "order", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "my",
               url: "{products}/{sp1}",
               defaults: new { controller = "products", action = "sp1", id = UrlParameter.Optional }
           );
        }
    }
}
