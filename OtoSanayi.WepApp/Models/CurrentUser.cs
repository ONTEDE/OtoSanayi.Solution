using OtoSanayi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoSanayi.WepApp.Models
{
    public class CurrentUser
    {
        public static string GetUser()
        {
            if (HttpContext.Current.Session["login"] != null)
            {
                Kullanici user = HttpContext.Current.Session["login"] as Kullanici;
                return user.KullaniciAdi;
            }
            else return "default";
        }

        public static void SetUser()
        {
            Kullanici user = HttpContext.Current.Session["login"] as Kullanici;
        }
    }
}