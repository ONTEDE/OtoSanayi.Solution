using OtoSanayi.DataAccessLayer;
using OtoSanayi.Entities;
using OtoSanayi.WepApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace OtoSanayi.WepApp.Controllers
{
    public class HomeController : Controller
    {
        UserManager _managerUser = new UserManager();
        FirmaManager _managerFirma = new FirmaManager();
        HaberManager _managerHaber = new HaberManager();
        IlanManager _managerIlan = new IlanManager();
        DuyuruManager _managerDuyuru = new DuyuruManager();

        
        // GET: Home
        public ActionResult Index()
        {
           

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanici model)
        {
            ModelState.Remove("Adi");
                ModelState.Remove("Soyadi");
            if (ModelState.IsValid)
            { 
                
                Kullanici kul = _managerUser.Find(x=>x.KullaniciAdi==model.KullaniciAdi&&x.Parola==model.Parola);
                if (kul==null)
                {
                    ModelState.AddModelError("","Kullanıcı Adı veya Şifre Hatalı");
                    return View(model);
                }
                Session["login"] = kul;
                
                CurrentUser.SetUser();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        public ActionResult AramaSonuc(string ara)
        {
            AraViewModels result = new AraViewModels();
            if (ara != null)
            {
                result.firmalist = _managerFirma.List(x => x.FirmaAdi.Contains(ara));
                result.haberlist = _managerHaber.List(x => x.HaberBaslik.Contains(ara)||x.HaberIcerik.Contains(ara));
                result.ilanlist = _managerIlan.List(x => x.Baslik.Contains(ara) || x.Aciklama.Contains(ara));
                result.duyurulist = _managerDuyuru.List(x => x.DuyuruBaslik.Contains(ara) || x.DuyuruIcerik.Contains(ara));

            }

            return View(result);
        }

        public ActionResult SiteMapOlustur() {
            DatabaseContext Veri = new DatabaseContext();

            Response.Clear();
            //Response.ContentTpye ile bu Action'ın View'ını XML tabanlı olarak ayarlıyoruz.
            Response.ContentType = "text/xml";
            string xmlPath = Server.MapPath("~/sitemap.xml");
            //Response.OutputStream
            XmlTextWriter xr = new XmlTextWriter(xmlPath, Encoding.UTF8);
            xr.Formatting = Formatting.Indented;

            xr.WriteStartDocument();
            xr.WriteStartElement("urlset");//urlset etiketi açıyoruz
            xr.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            xr.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xr.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/siteindex.xsd");
            /* sitemap dosyamızın olmazsa olmazını ekledik. Şeması bu dedik buraya kadar.  */

            xr.WriteStartElement("url");
            xr.WriteElementString("loc", "http://teknikotosanayi.com/");
            xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
            xr.WriteElementString("changefreq", "weekly");
            xr.WriteElementString("priority", "1");
            xr.WriteEndElement();

            //Burada veritabanımızdaki Firmaları SiteMap'e ekliyoruz.
            var s = Veri.Firmalar.ToList();
            foreach (var a in s)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://teknikotosanayi.com/" +AdGetir.LinkAd(a.SeoName));
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "0.90");
                xr.WriteElementString("changefreq", "weekly");
                xr.WriteEndElement();
            }

            //Aynı şekilde burada da Haberleri SiteMap'e ekliyoruz.
            var k = Veri.Haberler;
            foreach (var b in k)
            {
                xr.WriteStartElement("url");
                xr.WriteElementString("loc", "http://teknikotosanayi.com/Haber/DetayGetir/"+@AdGetir.LinkAd(b.HaberBaslik)+"/" + b.ID);
                xr.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                xr.WriteElementString("priority", "0.90");
                xr.WriteElementString("changefreq", "weekly");
                xr.WriteEndElement();
            }

            xr.WriteEndDocument();
            
            //urlset etiketini kapattık
            xr.Flush();
            xr.Close();
            Response.End();

            return View("Index");
            
        }


    }

}
