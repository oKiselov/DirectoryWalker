using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DirectoryWalker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "notdefault", 
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "CustomError", id =  UrlParameter.Optional});

            routes.MapRoute(
                name: "Default",
                url: "{*enteredPath}",
                defaults: new { controller = "Home", action = "BrowseHierarchyTree" });


        }
    }
}
