using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OtoSanayi.WepApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
                  name: "Anasayfa",
                  url: "Anasayfa",
                  defaults: new { controller = "Home", action = "Index" }
              );
            routes.MapRoute(
                 name: "Login",
                 url: "Login",
                 defaults: new { controller = "Home", action = "Login" }
             );
            routes.MapRoute(
                name: "Iletisim",
                url: "iletişim",
                defaults: new { controller = "Iletisim", action = "Index" }
            );
            routes.MapRoute(
                  name: "Default",
                  url: "{controller}/{action}/{id}",
                  defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
              );
            routes.MapRoute(
                   name: "Default2",
                   url: "{controller}/{action}/{Aciklama}/{id}",
                   defaults: new { controller = "Home", action = "Index", Aciklama="TOSS", id = UrlParameter.Optional }
               );

          

        }
    }
}
