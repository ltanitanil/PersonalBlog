using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PersonalBlog.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
              "",
              new { controller = "Post", action = "List", tag = (string)null, page = 1}
            );

            routes.MapRoute(null,
              "Кабинет",
              new { controller = "BlogManager", action = "Manager"}
            );

            routes.MapRoute(null,
              "Страница_{page}",
              new { controller = "Post", action = "List", tag = (string)null, wordsearch = (string)null, filter= (string)null },
              new { page = @"\d+" }
            );




            routes.MapRoute(null,
              "{tag}",
              new { controller = "Post", action = "List", page = 1 }
            );

            routes.MapRoute(null,
              "{tag}/Страница_{page}",
              new { controller = "Post", action = "List" },
              new { page = @"\d+" }
            );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
