using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yackimo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProfileSearch",
                url: "Profile/Details/{username}",
                defaults: new { controller = "Profile", action = "Details" }
            );
            
            //routes.MapRoute(
            //    name: "ProductSearch",
            //    url: "Home/{productname}",
            //    defaults: new { controller = "Home", action = "Search", productname = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}