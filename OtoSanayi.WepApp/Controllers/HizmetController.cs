using OtoSanayi.DataAccessLayer;
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
    public class HizmetController : Controller
    {
        HizmetManager _managerHizmet = new HizmetManager();
        HizmetKategoriManager _managerHizmetKategori = new HizmetKategoriManager();
        HizmetResimManager _managerHizmetResim = new HizmetResimManager();
        [AuthAdmin]
        public ActionResult Index()
        {

            return View(_managerHizmet.ListQueryable().Include(x => x.kategori).Include(c => c.hizmetResimler).OrderByDescending(x=>x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
            ViewBag.HizmetKategoriID = new SelectList(_managerHizmetKategori.List(), "ID", "HizmetKategoriAdi");

            return View();
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Ekle(Hizmet model, IEnumerable<HttpPostedFileBase> HizmetResimler)
		{
			ViewBag.HizmetKategoriID = new SelectList(_managerHizmetKategori.List(), "ID", "HizmetKategoriAdi");
			ViewBag.FirmaKategoriID = new SelectList(_managerHizmetKategori.List(), "ID", "HizmetKategoriAdi");
			ModelState.Remove("hizmetResimler");
            if (ModelState.IsValid)
            {
                HizmetKategori kat = _managerHizmetKategori.Find(x => x.ID == model.HizmetKategoriID);
                model.kategori = kat;
                
                int res = _managerHizmet.Insert(model);

                if (res == 0)
                {
                    ModelState.AddModelError("", "Hizmet Eklenemedi");
                    ViewBag.FirmaKategoriID = new SelectList(_managerHizmetKategori.List(), "ID", "HizmetKategoriAdi");
                    return View(model);
                }

                if (HizmetResimler!=null&&HizmetResimler.Count() > 0)
                {
                    foreach (HttpPostedFileBase file in HizmetResimler)
                    {
                        if (file != null &&
                   (file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png"))
                        {
                            try
                            {
                                HizmetResim rsm = new HizmetResim();
                                string filename = $"{AdGetir.ResimAd(model.HizmetBaslik)}.{file.ContentType.Split('/')[1]}";
                                file.SaveAs(Server.MapPath($"~/img/Hizmet/{filename}"));

                                rsm.ResimYol = filename;
                                rsm.HizmetID = model.ID;
                                _managerHizmetResim.Add(rsm);

                            }
                            catch (Exception exp)
                            {
                                ModelState.AddModelError("", exp.Message);
                            }
                        }

                    }
                    _managerHizmetResim.Save();
                }
                return RedirectToAction("Index");

            }
            ViewBag.FirmaKategoriID = new SelectList(_managerHizmetKategori.List(), "ID", "HizmetKategoriAdi");
            return View();
        }
        [AuthAdmin]
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hizmet frm = _managerHizmet.Find(x => x.ID == id.Value);
            if (frm == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            ViewBag.ID = new SelectList(_managerHizmetKategori.List(), "ID", "HizmetKategoriAdi");

            return View(frm);
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult Duzenle(Hizmet model,IEnumerable<HttpPostedFileBase> HizmetResimler)
        {
            ModelState.Remove("HizmetResimler");
			ViewBag.ID = new SelectList(_managerHizmetKategori.List(), "ID", "HizmetKategoriAdi");
			if (ModelState.IsValid)
            {
                Hizmet hzmt = _managerHizmet.Find(x => x.ID == model.ID);
                HizmetKategori kat = _managerHizmetKategori.Find(x => x.ID == model.HizmetKategoriID);


                hzmt.HizmetBaslik = model.HizmetBaslik;
                hzmt.HizmetIcerik = model.HizmetIcerik;
                hzmt.KisaHizmetIcerik = model.KisaHizmetIcerik;
                hzmt.HizmetKategoriID = kat.ID;
                hzmt.kategori = kat;
                int res = _managerHizmet.Update(hzmt);

                if (res == 0)
                {
                    ModelState.AddModelError("", "Hizmet Güncellenemedi");
                    ViewBag.ID = new SelectList(_managerHizmetKategori.List(), "ID", "HizmetKategoriAdi");
                    return View(model);
                }

                if (HizmetResimler != null && HizmetResimler.Count() > 0)
                {

                    foreach (HttpPostedFileBase file in HizmetResimler)
                    {
                        if (file != null &&
                   (file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png"))
                        {
                            try
                            {
                                HizmetResim rsm = new HizmetResim();
                                string filename = $"{AdGetir.ResimAd(hzmt.HizmetBaslik)}.{file.ContentType.Split('/')[1]}";
                                file.SaveAs(Server.MapPath($"~/img/Hizmet/{filename}"));

                                rsm.ResimYol = filename;
                                rsm.HizmetID = hzmt.ID;
                                _managerHizmetResim.Add(rsm);

                            }
                            catch (Exception exp)
                            {
                                ModelState.AddModelError("", exp.Message);
                            }
                        }

                    }
                    _managerHizmetResim.Save();

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

            Hizmet hzm = _managerHizmet.Find(x => x.ID == id.Value);
            if (hzm == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            ViewBag.ID = new SelectList(_managerHizmetKategori.List(), "ID", "HizmetKategoriAdi");

            return View(hzm);
        }
        [AuthAdmin]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hizmet hzm = _managerHizmet.Find(x => x.ID == id.Value);
            if (hzm == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            foreach (HizmetResim item in hzm.hizmetResimler)
            {
                if (System.IO.File.Exists(Server.MapPath("~/img/Hizmet/" + item.ResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath("~/img/Hizmet/" + item.ResimYol));
                }
            }
            _managerHizmet.Delete(hzm);

            return RedirectToAction("Index");
        }

        [AuthAdmin]
        public ActionResult KategoriListele()
        {
            return View(_managerHizmetKategori.List());
        }
        [AuthAdmin]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult KategoriEkle(HizmetKategori model)
        {
            if (ModelState.IsValid)
            {
                HizmetKategori kat = _managerHizmetKategori.Find(x => x.HizmetKategoriAdi == model.HizmetKategoriAdi);
                if (kat != null)
                {
                    ModelState.AddModelError("", "Ketegori Mevcut");
                    return View(model);
                }
                int res = _managerHizmetKategori.Insert(model);
                if (res == 0)
                {
                    ModelState.AddModelError("", "Ketegori Eklememedi");
                    return View(model);
                }
                return View("KategoriListele", _managerHizmetKategori.List());
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

            HizmetKategori kat = _managerHizmetKategori.Find(x => x.ID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(kat);
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult KategoriDuzenle(HizmetKategori model)
        {
            if (ModelState.IsValid)
            {
                HizmetKategori kat = _managerHizmetKategori.Find(x => x.ID == model.ID);
                kat.HizmetKategoriAdi = model.HizmetKategoriAdi;

                int res = _managerHizmetKategori.Update(kat);
                if (res > 0)
                {
                    return View("KategoriListele", _managerHizmetKategori.List());

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

            HizmetKategori frm = _managerHizmetKategori.Find(x => x.ID == id.Value);
            if (frm == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            _managerHizmetKategori.Delete(frm);

            return View("KategoriListele", _managerHizmetKategori.List());
        }

        public JsonResult ResimSil(int? id)
        {
            if (id == null)
            {
                return Json(0);
            }
            HizmetResim rsm = _managerHizmetResim.Find(x => x.ID == id);
            if (rsm == null)
            {
                return Json(0);
            }

            int rs = _managerHizmetResim.Delete(rsm);

            if (System.IO.File.Exists(Server.MapPath("~/img/Hizmet/" + rsm.ResimYol)))
            {
                System.IO.File.Delete(Server.MapPath("~/img/Hizmet/" + rsm.ResimYol));
            }

            return Json(new { result = rs }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ResimListele(int? id)
        {
            List<HizmetResim> resimlist = _managerHizmetResim.List(x => x.HizmetID == id);

            return PartialView("_PartialResimListele", resimlist);
        }

        public PartialViewResult HizmetKategoriDoldur()
        {
            return PartialView("_PartialHizmetMenuGetir",_managerHizmetKategori.List());
        }

        public ActionResult DetayGetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hizmet kat = _managerHizmet.List(x => x.HizmetKategoriID == id.Value).OrderByDescending(x => x.ID).FirstOrDefault();
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            return View(kat);
        }
    }
}