using OtoSanayi.BusinessLayer;
using OtoSanayi.Entities;
using OtoSanayi.WepApp.Filter;
using OtoSanayi.WepApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OtoSanayi.WepApp.Controllers
{
    [ValidateInput(false)]
    public class HaberController : Controller
    {
        HaberManager _managerHaber = new HaberManager();
        HaberResimManager _managerHaberResim = new HaberResimManager();
        [AuthAdmin]
        public ActionResult Index()
        {
            return View(_managerHaber.ListQueryable().OrderByDescending(x => x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult Ekle(Haber model,IEnumerable<HttpPostedFileBase> HaberResimler)
        {
            ModelState.Remove("HaberResimler");
            if (ModelState.IsValid)
            {
              int res=_managerHaber.Insert(model);
                if (res==0)
                {
                    ModelState.AddModelError("","Haber Eklenemedi");
                    return View(model);
                }
               

                if (HaberResimler!=null&&HaberResimler.Count() > 0)
                {
                    foreach (HttpPostedFileBase file in HaberResimler)
                    {
                        if (file != null &&
                   (file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png"))
                        {
                            try
                            {
                                HaberResim rsm = new HaberResim();
                                string filename = $"{AdGetir.ResimAd(model.HaberBaslik)}.{file.ContentType.Split('/')[1]}";
                                file.SaveAs(Server.MapPath($"~/img/Haber/{filename}"));
                                rsm.haber = model;
                                rsm.ResimYol = filename;
                                rsm.HaberID = model.ID;
                                _managerHaberResim.Add(rsm);

                            }
                            catch (Exception exp)
                            {
                                ModelState.AddModelError("", exp.Message);
                            }
                        }

                    }
                    _managerHaberResim.Save();
                }
                return RedirectToAction("Index","Haber");

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

            Haber hbr = _managerHaber.Find(x => x.ID == id.Value);
            if (hbr == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
                       

            return View(hbr);
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult Duzenle(Haber model, IEnumerable<HttpPostedFileBase> HaberResimler)
        {
            ModelState.Remove("HaberResimler");
            if (ModelState.IsValid)
            {
                Haber hbr = _managerHaber.Find(x => x.ID == model.ID);

                hbr.HaberBaslik = model.HaberBaslik;
                hbr.HaberIcerik = model.HaberIcerik;
                hbr.KisaHaberIcerik = model.KisaHaberIcerik;
                int res =_managerHaber.Update(hbr);

                if (res == 0)
                {
                    ModelState.AddModelError("", "Haber Güncellenemedi");
                    return View(model);
                }

                if (HaberResimler != null && HaberResimler.Count() > 0)
                {

                    foreach (HttpPostedFileBase file in HaberResimler)
                    {
                        if (file != null &&
                   (file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png"))
                        {
                            try
                            {
                                HaberResim rsm = new HaberResim();
                                string filename = $"{AdGetir.ResimAd(hbr.HaberBaslik)}.{file.ContentType.Split('/')[1]}";
                                file.SaveAs(Server.MapPath($"~/img/Haber/{filename}"));

                                rsm.ResimYol = filename;
                                rsm.HaberID = hbr.ID;
                                _managerHaberResim.Add(rsm);

                            }
                            catch (Exception exp)
                            {
                                ModelState.AddModelError("", exp.Message);
                            }
                        }

                    }
                    _managerHaberResim.Save();

                }
                return RedirectToAction("Index");


            }
            return View(model);
        }
        [AuthAdmin]
        public ActionResult Detay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Haber hbr = _managerHaber.Find(x => x.ID == id.Value);
            if (hbr == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            
            return View(hbr);
        }
        [AuthAdmin]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Haber hbr = _managerHaber.Find(x => x.ID == id.Value);
            if (hbr == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            foreach (var item in hbr.haberResimler)
            {
                if (System.IO.File.Exists(Server.MapPath("~/img/Haber/" + item.ResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath("~/img/Haber/" + item.ResimYol));
                }
            }
           int res= _managerHaber.Delete(hbr);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ResimSil(int? id)
        {
            if (id == null)
            {
                return Json(0);
            }
            HaberResim rsm = _managerHaberResim.Find(x => x.ID == id);
            if (rsm == null)
            {
                return Json(0);
            }

            int rs = _managerHaberResim.Delete(rsm);

            if (System.IO.File.Exists(Server.MapPath("~/img/Haber/" + rsm.ResimYol)))
            {
                System.IO.File.Delete(Server.MapPath("~/img/Haber/" + rsm.ResimYol));
            }

            return Json(new { result = rs }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ResimListele(int? id)
        {
            List<HaberResim> resimlist = _managerHaberResim.List(x => x.HaberID == id);

            return PartialView("_PartialResimListele", resimlist);
        }
        public ActionResult HaberListele()
        {
            return View(_managerHaber.List().OrderByDescending(x=>x.ID).ToList());
        }

        public PartialViewResult AnaHaberGetir()
        {
            return PartialView("_PartialAnaHaberGetir",_managerHaber.List().OrderByDescending(x=>x.ID).Take(3).ToList());
        }

        public ActionResult DetayGetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Haber kat = _managerHaber.Find(x => x.ID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            return View(kat);
        }
    }
}