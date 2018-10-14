using OtoSanayi.BusinessLayer;
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



    }
}