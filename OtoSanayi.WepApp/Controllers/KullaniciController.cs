using OtoSanayi.DataAccessLayer;
using OtoSanayi.Entities;
using OtoSanayi.WepApp.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OtoSanayi.WepApp.Controllers
{
    public class KullaniciController : Controller
    {
        UserManager _managerUser = new UserManager();
        [AuthAdmin]
        public ActionResult Index()
        {
            return View(_managerUser.ListQueryable().OrderByDescending(x => x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
            return View();
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Ekle(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                Kullanici kul = _managerUser.Find(x=>x.KullaniciAdi==model.KullaniciAdi);
                if (kul!=null)
                {
                    ModelState.AddModelError("","Kullanıcı Adı Mevcut");
                    return View(model);
                }
                kul = model;
               int res= _managerUser.Insert(kul);
                if (res>0)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("","Kullanıcı Eklenemedi");

            }
            return View(model);
        }
        [AuthAdmin]
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Kullanici kul = _managerUser.Find(x => x.ID == id.Value);
            if (kul == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(kul);
            
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Duzenle(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                Kullanici kul = _managerUser.Find(x=>x.ID==model.ID);
                kul.Adi = model.Adi;
                kul.Soyadi = model.Soyadi;
                kul.KullaniciAdi = model.KullaniciAdi;
                kul.Parola = model.Parola;
              int res= _managerUser.Update(kul);
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Kullanıcı Eklenemedi");
            }
            return View(model);
        }
        [AuthAdmin]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Kullanici kul = _managerUser.Find(x => x.ID == id.Value);
            if (kul == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

           _managerUser.Delete(kul);
            return RedirectToAction("Index");
        }
        
      
    }
}
