using OtoSanayi.BusinessLayer;
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
    
    [ValidateInput(false)]
    public class DuyuruController : Controller
    {
        DuyuruManager _managerDuyuru = new DuyuruManager();
        [AuthAdmin]
        public ActionResult Index()
        {
            return View(_managerDuyuru.List().OrderByDescending(x => x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Detay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Duyuru duy = _managerDuyuru.Find(x => x.ID == id.Value);
            if (duy == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(duy);
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
            return View();
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Ekle(Duyuru model)
        {
            ModelState.Remove("DuyuruTarih");
                if (ModelState.IsValid)
                {
                    Duyuru yeniduyuru = new Duyuru();
                    yeniduyuru.DuyuruBaslik = model.DuyuruBaslik;
                    yeniduyuru.DuyuruIcerik = model.DuyuruIcerik;
                yeniduyuru.KisaDuyuruIcerik = model.KisaDuyuruIcerik;
                yeniduyuru.DuyuruTarih = DateTime.Now;
                 int res=_managerDuyuru.Insert(yeniduyuru);

                    if (res > 0)
                    {
                    return RedirectToAction("Index");

                }
                    ModelState.AddModelError("", "Duyuru Eklenemedi");
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

            Duyuru duy = _managerDuyuru.Find(x => x.ID == id.Value);
            if (duy == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(duy);
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Duzenle(Duyuru model)
        {
            ModelState.Remove("DuyuruTarih");
            if (ModelState.IsValid)
            {
                Duyuru duy = _managerDuyuru.Find(x => x.ID == model.ID);
                duy.DuyuruBaslik = model.DuyuruBaslik;
                duy.DuyuruIcerik = model.DuyuruIcerik;
                duy.KisaDuyuruIcerik = model.KisaDuyuruIcerik;
                duy.DuyuruTarih = DateTime.Now;
               int res= _managerDuyuru.Update(duy);
                if (res>0)
                {
                    return RedirectToAction("Index");

                }
                ModelState.AddModelError("","Duyuru Güncellenemedi");

            }
            return View();
        }

        [AuthAdmin]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Duyuru duy = _managerDuyuru.Find(x => x.ID == id.Value);
            if (duy == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            _managerDuyuru.Delete(duy);
            return RedirectToAction("Index");
        }

        public ActionResult DuyuruListele()
        {

            return View(_managerDuyuru.List().OrderByDescending(x=>x.ID).ToList());
        }
        public ActionResult DetayGetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Duyuru> kat = _managerDuyuru.List(x => x.ID == id.Value).OrderByDescending(x => x.ID).ToList();
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            return View(kat);
        }
            
        

    }
}
