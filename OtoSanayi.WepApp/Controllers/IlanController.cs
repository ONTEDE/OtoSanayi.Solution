using OtoSanayi.BusinessLayer;
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
    [ValidateInput(false)]
    public class IlanController : Controller
    {
        IlanManager _managerIlan = new IlanManager();
        IlanKategoriManager _managerIlanKategori = new IlanKategoriManager();
        FirmaManager _managerFirma = new FirmaManager();
        IlanResimManager _managerIlanResim = new IlanResimManager();

        Ilan i = new Ilan();
        Firma f = new Firma();
        [AuthAdmin]
        public ActionResult Index()
        {

            return View(_managerIlan.List().OrderByDescending(x=>x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
           
            ViewBag.IlanKategoriID = new SelectList(_managerIlanKategori.List(), "IlanKategoriID", "KategoriAdi");
            ViewBag.FirmaID= new SelectList(_managerFirma.List(), "ID", "FirmaAdi");

            return View();
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Ekle(Ilan model, IEnumerable<HttpPostedFileBase> IlanResimler)
        {
            ModelState.Remove("IlanTarih");
            ModelState.Remove("ilanResimler");
            if (ModelState.IsValid)
            {
                IlanKategori kat = _managerIlanKategori.Find(x => x.IlanKategoriID == model.IlanKategoriID);
                Firma frm = _managerFirma.Find(x=>x.ID==model.FirmaID);
                model.kategori = kat;
                model.firma = frm;
                model.IlanTarih = DateTime.Now;

                int res = _managerIlan.Insert(model);

                if (res == 0)
                {
                    ModelState.AddModelError("", "İlan Eklenemedi");
                    ViewBag.IlanKategoriID = new SelectList(_managerIlanKategori.List(), "IlanKategoriID", "KategoriAdi");
                    ViewBag.FirmaID = new SelectList(_managerFirma.List(), "ID", "FirmaAdi");
                    return View(model);
                }
                if (IlanResimler!=null&&IlanResimler.Count() > 0)
                {

                    foreach (HttpPostedFileBase file in IlanResimler)
                    {
                        if (file != null &&
                   (file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png"))
                        {
                            try
                            {
                                IlanResim rsm = new IlanResim();
                                string filename = $"{AdGetir.ResimAd(model.Baslik)}.{file.ContentType.Split('/')[1]}";
                                file.SaveAs(Server.MapPath($"~/img/Ilan/{filename}"));

                                rsm.ResimYol = filename;
                                rsm.IlanID = model.ID;
                                _managerIlanResim.Add(rsm);

                            }
                            catch (Exception exp)
                            {
                                ModelState.AddModelError("", exp.Message);
                            }
                        }

                    }
                    _managerIlanResim.Save();

                }

                return RedirectToAction("Index");

            }
            ViewBag.IlanKategoriID = new SelectList(_managerIlanKategori.List(), "IlanKategoriID", "KategoriAdi");
            ViewBag.FirmaID = new SelectList(_managerFirma.List(), "ID", "FirmaAdi");
            return View();
        }
        [AuthAdmin]
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ilan ilan = _managerIlan.Find(x => x.ID == id.Value);
            if (ilan == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            ViewBag.IlanKategoriID = new SelectList(_managerIlanKategori.List(), "IlanKategoriID", "KategoriAdi");
            ViewBag.fID = new SelectList(_managerFirma.List(), "ID", "FirmaAdi");

            return View(ilan);
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult Duzenle(Ilan model,int fID, IEnumerable<HttpPostedFileBase> IlanResimler)
        {
            ViewBag.IlanKategoriID = new SelectList(_managerIlanKategori.List(), "IlanKategoriID", "KategoriAdi");
            ViewBag.fID = new SelectList(_managerFirma.List(), "ID", "FirmaAdi");
            ModelState.Remove("IlanTarih");
            ModelState.Remove("ilanResimler");
            if (ModelState.IsValid)
            {
                Ilan ilan = _managerIlan.Find(x => x.ID == model.ID);
                IlanKategori kat = _managerIlanKategori.Find(x => x.IlanKategoriID == model.IlanKategoriID);
                Firma frm = _managerFirma.Find(x => x.ID == fID);
                ilan.Baslik = model.Baslik;
                ilan.Aciklama = model.Aciklama;
                ilan.KisaAciklama = model.KisaAciklama;
                ilan.kategori = kat;
                ilan.firma = frm;
                ilan.IlanTarih = DateTime.Now;
               
                int res = _managerIlan.Update(ilan);

                if (res == 0)
                {
                    ModelState.AddModelError("", "İlan Güncellenemedi");
                    ViewBag.IlanKategoriID = new SelectList(_managerIlanKategori.List(), "IlanKategoriID", "KategoriAdi");
                    ViewBag.fID = new SelectList(_managerFirma.List(), "ID", "FirmaAdi");
                    return View(model);
                }

                if (IlanResimler != null && IlanResimler.Count() > 0)
                {

                    foreach (HttpPostedFileBase file in IlanResimler)
                    {
                        if (file != null &&
                   (file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png"))
                        {
                            try
                            {
                                IlanResim rsm = new IlanResim();
                                string filename = $"{AdGetir.ResimAd(ilan.Baslik)}.{file.ContentType.Split('/')[1]}";
                                file.SaveAs(Server.MapPath($"~/img/Ilan/{filename}"));

                                rsm.ResimYol = filename;
                                rsm.IlanID = ilan.ID;
                                _managerIlanResim.Add(rsm);

                            }
                            catch (Exception exp)
                            {
                                ModelState.AddModelError("", exp.Message);
                            }
                        }

                    }
                    _managerIlanResim.Save();

                }

                return RedirectToAction("Index");


            }
            
            return View();
        }
        [AuthAdmin]
        public ActionResult Detay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ilan ilan = _managerIlan.Find(x => x.ID == id.Value);
            if (ilan == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            ViewBag.IlanKategoriID = new SelectList(_managerIlanKategori.List(), "IlanKategoriID", "KategoriAdi");
            ViewBag.ID = new SelectList(_managerFirma.List(), "ID", "FirmaAdi");

            return View(ilan);
        }
        [AuthAdmin]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ilan ilan = _managerIlan.Find(x => x.ID == id.Value);
            if (ilan == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            foreach (var item in ilan.ilanResimler)
            {
                if (System.IO.File.Exists(Server.MapPath("~/img/Ilan/" + item.ResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath("~/img/Ilan/" + item.ResimYol));
                }
            }
            _managerIlan.Delete(ilan);

            return RedirectToAction("Index");
        }
        [AuthAdmin]
        public ActionResult KategoriListele()
        {
            return View(_managerIlanKategori.List());
        }
        [AuthAdmin]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult KategoriEkle(IlanKategori model)
        {
            if (ModelState.IsValid)
            {
                IlanKategori kat = _managerIlanKategori.Find(x => x.KategoriAdi == model.KategoriAdi);
                if (kat != null)
                {
                    ModelState.AddModelError("", "Ketegori Mevcut");
                    return View(model);
                }
                int res = _managerIlanKategori.Insert(model);
                if (res == 0)
                {
                    ModelState.AddModelError("", "Ketegori Eklememedi");
                    return View(model);
                }
                return View("KategoriListele", _managerIlanKategori.List());
            }
            return View(model);
        }
        [AuthAdmin]
        public ActionResult KategoriDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IlanKategori kat = _managerIlanKategori.Find(x => x.IlanKategoriID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(kat);
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult KategoriDuzenle(IlanKategori model)
        {
            if (ModelState.IsValid)
            {
                IlanKategori kat = _managerIlanKategori.Find(x => x.IlanKategoriID == model.IlanKategoriID);
                kat.KategoriAdi = model.KategoriAdi;

                int res = _managerIlanKategori.Update(kat);
                if (res > 0)
                {
                    return View("KategoriListele", _managerIlanKategori.List());

                }
                ModelState.AddModelError("", "Kategori Güncellenemedi");

            }
            return View(model);
        }
        [AuthAdmin]
        public ActionResult KategoriSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IlanKategori kat = _managerIlanKategori.Find(x => x.IlanKategoriID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            _managerIlanKategori.Delete(kat);

            return View("KategoriListele", _managerIlanKategori.List());
        }

        public JsonResult ResimSil(int? id)
        {
            if (id == null)
            {
                return Json(0);
            }
            IlanResim rsm = _managerIlanResim.Find(x => x.ID == id);
            if (rsm == null)
            {
                return Json(0);
            }

            int rs = _managerIlanResim.Delete(rsm);

            if (System.IO.File.Exists(Server.MapPath("~/img/Ilan/" + rsm.ResimYol)))
            {
                System.IO.File.Delete(Server.MapPath("~/img/Ilan/" + rsm.ResimYol));
            }

            return Json(new { result = rs }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ResimListele(int? id)
        {
            List<IlanResim> resimlist = _managerIlanResim.List(x => x.IlanID == id);

            return PartialView("_PartialResimListele", resimlist);
        }

        public PartialViewResult AnaIlanGetir()
        {
            ViewIlan iln = new ViewIlan();

            iln.Ilanlar = _managerIlan.List().OrderByDescending(x => x.ID).ToList();
            iln.Kategoriler = _managerIlanKategori.List();
            return PartialView("_PartiaAnaIlanGetir",iln);
        }

        public PartialViewResult UstIlanGetir()
        {

            return PartialView("_PartialUstIlanGetir", _managerIlan.List().OrderByDescending(x=>x.ID).Take(20).ToList());
        }

        public ActionResult IlanListele()
        {
            return View(_managerIlan.List().OrderByDescending(x=>x.ID).ToList());
        }

        public ActionResult DetayGetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ilan kat = _managerIlan.Find(x => x.ID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            return View(kat);
        }
    }
}