using OtoSanayi.DataAccessLayer;
using OtoSanayi.Entities;
using OtoSanayi.WepApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

    }
}