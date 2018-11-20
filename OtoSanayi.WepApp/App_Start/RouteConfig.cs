using OtoSanayi.Entities;
using OtoSanayi.WepApp.Models;
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

            SeoFirmaListele list = new SeoFirmaListele();
          
            foreach (var item in list.firmaGetir())
            {
                routes.MapRoute(
                 name: AdGetir.LinkAd(item.SeoName).ToString(),
                 url: AdGetir.LinkAd(item.SeoName).ToString(),
                 defaults: new { controller = "Firma", action = "DetayGetir", id = item.ID }
             );
            }

            //   routes.MapRoute(
            //    name: "Aslı-Kafe",
            //    url: "Aslı-Kafe",
            //    defaults: new { controller = "Firma", action = "DetayGetir",id="5" }
            //);



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
                 name: "HataSayfasi",
                 url: "HataSayfasi",
                 defaults: new { controller = "Home", action = "AccessDenied" }
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
                   defaults: new { controller = "Home", action = "Index", Aciklama = "TOSS", id = UrlParameter.Optional }
               );



        }
    }
}
