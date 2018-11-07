using OtoSanayi.DataAccessLayer;
using OtoSanayi.Entities;
using OtoSanayi.WepApp.Filter;
using OtoSanayi.WepApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OtoSanayi.WepApp.Controllers
{
    public class ReklamController : Controller
    {
        ReklamManager _managerReklam = new ReklamManager();
        // GET: Reklam
        [AuthAdmin]
        public ActionResult Index()
        {
            return View(_managerReklam.List().OrderByDescending(x => x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult Ekle(Reklam model, HttpPostedFileBase ReklamResim)
        {
            ModelState.Remove("ResimYol");
            if (ModelState.IsValid)
            {



                if (ReklamResim != null &&
                  (ReklamResim.ContentType == "image/jpeg" ||
                  ReklamResim.ContentType == "image/jpg" ||
                  ReklamResim.ContentType == "image/png"))
                {
                    try
                    {

                        string filename = $"{AdGetir.ResimAd(model.ReklamAdi)}.{ReklamResim.ContentType.Split('/')[1]}";
                        ReklamResim.SaveAs(Server.MapPath($"~/img/Reklam/{filename}"));
                        model.ResimYol = filename;

                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("", exp.Message);
                    }
                }

                int res = _managerReklam.Insert(model);
                if (res > 0)
                {
                    return RedirectToAction("Index", "Reklam");

                }
                ModelState.AddModelError("", "Kategori Güncellenemedi");
                return View(model);
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

            Reklam kat = _managerReklam.Find(x => x.ID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(kat);
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Duzenle(Reklam model, HttpPostedFileBase ReklamResim)
        {
            ModelState.Remove("ResimYol");
            if (ModelState.IsValid)
            {
                Reklam kat = _managerReklam.Find(x => x.ID == model.ID);
                kat.ReklamAdi = model.ReklamAdi;
                kat.ReklamCesit = model.ReklamCesit;
                kat.ReklamLink = model.ReklamLink;
                if (ReklamResim != null &&
                  (ReklamResim.ContentType == "image/jpeg" ||
                  ReklamResim.ContentType == "image/jpg" ||
                  ReklamResim.ContentType == "image/png"))
                {
                    try
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/img/Reklam/" + model.ResimYol)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/img/Reklam/" + model.ResimYol));
                        }


                        string filename = $"{AdGetir.ResimAd(model.ReklamAdi)}.{ReklamResim.ContentType.Split('/')[1]}";
                        ReklamResim.SaveAs(Server.MapPath($"~/img/Reklam/{filename}"));
                        kat.ResimYol = filename;

                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("", exp.Message);
                    }
                }

                int res = _managerReklam.Update(kat);
                if (res > 0)
                {
                    return RedirectToAction("Index","Reklam");

                }
                ModelState.AddModelError("", "Kategori Güncellenemedi");

            }
            return View(model);
        }
        [AuthAdmin]
        public ActionResult ReklamSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reklam frm = _managerReklam.Find(x => x.ID == id.Value);
            if (frm == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            try
            {
                if (System.IO.File.Exists(Server.MapPath("~/img/Reklam/" + frm.ResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath("~/img/Reklam/" + frm.ResimYol));
                }

                _managerReklam.Delete(frm);
            }
            catch { }

            return RedirectToAction("Index", "Reklam");
        }

        public PartialViewResult UstReklamGetir()
        {
            return PartialView("_PartialUstReklam",_managerReklam.List(x=>x.ReklamCesit==1).OrderByDescending(x=>x.ID).Take(1).ToList());
        }
        public PartialViewResult AltReklamGetir()
        {
            return PartialView("_PartialAltReklam", _managerReklam.List(x => x.ReklamCesit == 2).OrderByDescending(x => x.ID).Take(1).ToList());
        }
    }
}