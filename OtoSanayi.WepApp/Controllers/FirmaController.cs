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
    public class FirmaController : Controller
    {
        FirmaManager _managerFirma = new FirmaManager();
        FirmaKategoriManager _managerFirmaKategori = new FirmaKategoriManager();
        FirmaResimManager _managerFirmaResim = new FirmaResimManager();
        FirmaKategoriBagManager _managerFirmaKategoriBag = new FirmaKategoriBagManager();

        [AuthAdmin]
        public ActionResult Index()
        {

            return View(_managerFirma.List().OrderByDescending(x => x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
            //ViewBag.FirmaKategoriID = new SelectList(_managerFirmaKategori.List(), "ID", "KategoriAdi");

            KategoriFirmaViewModel kat = new KategoriFirmaViewModel();
            kat.FirmaKategoriler = _managerFirmaKategori.List();

            return View(kat);
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(KategoriFirmaViewModel model, HttpPostedFileBase FirmaLogo, IEnumerable<HttpPostedFileBase> FirmaResimler)
        {
            //ModelState.Remove("FirmaAdi");
            //ModelState.Remove("FirmaTel");
            //ModelState.Remove("Adres");
            ModelState.Clear();
            //ModelState.Remove("KategoriAdi");
            //ModelState.Remove("KategoriAciklama");

            if (ModelState.IsValid)
            {
                int i = KontrolEt(model);

                if (i == 1)
                {
                    //model.FirmaKategoriler = _managerFirmaKategori.List();
                    //ViewBag.FirmaKategoriID = new SelectList(_managerFirmaKategori.List(), "ID", "KategoriAdi");
                    model.FirmaKategoriler = _managerFirmaKategori.List();
                    return View(model);
                }

                //FirmaKategori kat = _managerFirmaKategori.Find(x => x.ID == FirmaKategoriID);
                //model.Firma.Kategori = kat;

                #region Firma Logo Ekleme
                if (FirmaLogo != null &&
                        (FirmaLogo.ContentType == "image/jpeg" ||
                        FirmaLogo.ContentType == "image/jpg" ||
                        FirmaLogo.ContentType == "image/png"))
                {
                    try
                    {
                        string filename = $"{AdGetir.ResimAd(model.Firma.FirmaAdi)}.{FirmaLogo.ContentType.Split('/')[1]}";
                        FirmaLogo.SaveAs(Server.MapPath($"~/img/Firma/{filename}"));
                        model.Firma.Logo = filename;

                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("", exp.Message);
                    }
                }
                #endregion
                int res = _managerFirma.Insert(model.Firma);
                if (res == 0)
                {
                    ModelState.AddModelError("", "Firma Eklenemedi");
                    //model.FirmaKategoriler = _managerFirmaKategori.List();
                    //ViewBag.FirmaKategoriID = new SelectList(_managerFirmaKategori.List(), "ID", "KategoriAdi");
                    return View(model);
                }
                foreach (FirmaKategori item in model.FirmaKategoriler)
                {
                    if (item.Secili)
                    {
                        Firma_Kategori1 fk = new Firma_Kategori1();
                        fk.FirmaKategori = _managerFirmaKategori.Find(x => x.ID == item.ID);
                        fk.Firma = _managerFirma.Find(x => x.ID == model.Firma.ID);

                        _managerFirmaKategoriBag.Add(fk);
                    }
                }
                _managerFirmaKategoriBag.Save();

                #region Firma Resimler Ekleme
                if (FirmaResimler != null && FirmaResimler.Count() > 0)
                {
                    foreach (HttpPostedFileBase file in FirmaResimler)
                    {
                        if (file != null &&
                   (file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png"))
                        {
                            try
                            {
                                FirmaResim rsm = new FirmaResim();
                                string filename = $"{AdGetir.ResimAd(model.Firma.FirmaAdi)}.{file.ContentType.Split('/')[1]}";
                                file.SaveAs(Server.MapPath($"~/img/Firma/{filename}"));

                                rsm.ResimYol = filename;
                                rsm.FirmaID = model.Firma.ID;
                                _managerFirmaResim.Add(rsm);

                            }
                            catch (Exception exp)
                            {
                                ModelState.AddModelError("", exp.Message);
                            }
                        }

                    }
                    _managerFirmaResim.Save();
                }
                #endregion
                return RedirectToAction("Index");

            }
            model.FirmaKategoriler = _managerFirmaKategori.List();
            //ViewBag.FirmaKategoriID = new SelectList(_managerFirmaKategori.List(), "ID", "KategoriAdi");
            return View(model);
        }

        private int KontrolEt(KategoriFirmaViewModel firma)
        {
            int i = 0;

            if (firma.Firma.FirmaAdi == null)
            {
                i = 1;
                ModelState.AddModelError("", "Firma Adı Gerekli");
            }
            if (firma.Firma.FirmaTel == null)
            {
                i = 1;
                ModelState.AddModelError("", "Firma Telefon Gerekli");
            }
            if (firma.Firma.Adres == null)
            {
                i = 1;
                ModelState.AddModelError("", "Firma Adres Gerekli");
            }
            int s = 0;
            foreach (var item in firma.FirmaKategoriler)
            {
                if (item.Secili)
                {
                    s++;
                }
            }
            if (s == 0)
            {
                i = 1;
                ModelState.AddModelError("", "En Az Bir Kategori Seçmek Zorundasınız.");
            }

            return i;
        }

        [AuthAdmin]
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KategoriFirmaViewModel kf = new KategoriFirmaViewModel();
            kf.Firma = _managerFirma.Find(x => x.ID == id.Value);
            kf.FirmaKategoriler = _managerFirmaKategori.List();
            if (kf.Firma == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            Firma_Kategori1 kt = new Firma_Kategori1();
            foreach (var item in kf.FirmaKategoriler)
            {
                kt = null;
                kt = _managerFirmaKategoriBag.Find(x => x.Firma.ID == id && x.FirmaKategori.ID == item.ID);
                if (kt != null)
                {
                    item.Secili = true;
                }
                else
                    item.Secili = false;

            }


            return View(kf);
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult Duzenle(KategoriFirmaViewModel model, HttpPostedFileBase FirmaLogo, IEnumerable<HttpPostedFileBase> FirmaResimler)
        {
            //ViewBag.ID = new SelectList(_managerFirmaKategori.List(), "ID", "KategoriAdi");
            //ModelState.Remove("FirmaResimler");
            //ModelState.Remove("Logo");
            ModelState.Clear();
            if (ModelState.IsValid)
            {

                int i = KontrolEt(model);
                if (i == 1)
                {
                    model.FirmaKategoriler = _managerFirmaKategori.List();
                    return View(model);
                }

                Firma frm = _managerFirma.Find(x => x.ID == model.Firma.ID);
                //FirmaKategori kat = _managerFirmaKategori.Find(x => x.ID == model.FirmaKategoriID);

                #region Firma_Logo_Ekleme
                if (FirmaLogo != null &&
                       (FirmaLogo.ContentType == "image/jpeg" ||
                       FirmaLogo.ContentType == "image/jpg" ||
                       FirmaLogo.ContentType == "image/png"))
                {
                    try
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/img/Firma/" + model.Firma.Logo)))
                        {
                            System.IO.File.Delete(Server.MapPath("~/img/Firma/" + model.Firma.Logo));
                        }

                        string filename = $"{AdGetir.ResimAd(model.Firma.FirmaAdi)}.{FirmaLogo.ContentType.Split('/')[1]}";
                        FirmaLogo.SaveAs(Server.MapPath($"~/img/Firma/{filename}"));
                        frm.Logo = filename;

                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("", exp.Message);
                    }
                }
                #endregion
                frm.Aciklama = model.Firma.Aciklama;
                frm.KisaAciklama = model.Firma.KisaAciklama;
                frm.Adres = model.Firma.Adres;
                frm.FirmaAdi = model.Firma.FirmaAdi;
                //frm.Kategori = kat;
                //frm.FirmaKategoriID = model.FirmaKategoriID;
                frm.FirmaLink = model.Firma.FirmaLink;
                frm.Aktif = model.Firma.Aktif;
                frm.FirmaTel = model.Firma.FirmaTel;
                frm.FirmaMail = model.Firma.FirmaMail;
                frm.FirmaHarita = model.Firma.FirmaHarita;
                frm.FirmaFace = model.Firma.FirmaFace;
                frm.FirmaTwitter = model.Firma.FirmaTwitter;
                frm.FirmaGoogle = model.Firma.FirmaGoogle;
                frm.FirmaInstagram = model.Firma.FirmaInstagram;

                int res = _managerFirma.Update(frm);

                if (res == 0)
                {
                    ModelState.AddModelError("", "Firma Güncellenemedi");
                    //ViewBag.ID = new SelectList(_managerFirmaKategori.List(), "ID", "KategoriAdi");
                    return View(model);
                }

                foreach (var itm in _managerFirmaKategoriBag.List(x => x.Firma.ID == frm.ID))
                {
                    _managerFirmaKategoriBag.Delete(itm);
                }
                foreach (FirmaKategori item in model.FirmaKategoriler)
                {

                    if (item.Secili)
                    {
                        Firma_Kategori1 fk = new Firma_Kategori1();
                        fk.FirmaKategori = _managerFirmaKategori.Find(x => x.ID == item.ID);
                        fk.Firma = _managerFirma.Find(x => x.ID == model.Firma.ID);

                        _managerFirmaKategoriBag.Add(fk);
                    }
                }
                _managerFirmaKategoriBag.Save();


                #region FirmaResimler
                if (FirmaResimler != null && FirmaResimler.Count() > 0)
                {

                    foreach (HttpPostedFileBase file in FirmaResimler)
                    {
                        if (file != null &&
                   (file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png"))
                        {
                            try
                            {
                                FirmaResim rsm = new FirmaResim();
                                string filename = $"{AdGetir.ResimAd(model.Firma.FirmaAdi)}.{file.ContentType.Split('/')[1]}";
                                file.SaveAs(Server.MapPath($"~/img/Firma/{filename}"));

                                rsm.ResimYol = filename;
                                rsm.FirmaID = frm.ID;
                                _managerFirmaResim.Add(rsm);

                            }
                            catch (Exception exp)
                            {
                                ModelState.AddModelError("", exp.Message);
                            }
                        }

                    }
                    _managerFirmaResim.Save();

                }
                #endregion
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

            Firma frm = _managerFirma.Find(x => x.ID == id.Value);
            if (frm == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            ViewBag.ID = new SelectList(_managerFirmaKategori.List(), "ID", "KategoriAdi");

            return View(frm);
        }
        [AuthAdmin]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Firma frm = _managerFirma.Find(x => x.ID == id.Value);
            if (frm == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            if (System.IO.File.Exists(Server.MapPath("~/img/Firma/" + frm.Logo)))
            {
                System.IO.File.Delete(Server.MapPath("~/img/Firma/" + frm.Logo));
            }
            foreach (var item in _managerFirmaKategoriBag.List(x => x.Firma.ID == id))
            {
                _managerFirmaKategoriBag.Delete(item);
            }
            _managerFirma.Delete(frm);

            return RedirectToAction("Index");
        }

        [AuthAdmin]
        public ActionResult KategoriListele()
        {
            return View(_managerFirmaKategori.List());
        }
        [AuthAdmin]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult KategoriEkle(FirmaKategori model, HttpPostedFileBase KategoriLogo)
        {

            if (ModelState.IsValid)
            {
                FirmaKategori kat = _managerFirmaKategori.Find(x => x.KategoriAdi == model.KategoriAdi);

                if (kat != null)
                {
                    ModelState.AddModelError("", "Ketegori Mevcut");
                    return View(model);
                }
                //  model.KategoriResim = "default.jpg";
                if (KategoriLogo != null &&
                  (KategoriLogo.ContentType == "image/jpeg" ||
                  KategoriLogo.ContentType == "image/jpg" ||
                  KategoriLogo.ContentType == "image/png"))
                {
                    try
                    {

                        string filename = $"{AdGetir.ResimAd(model.KategoriAdi)}.{KategoriLogo.ContentType.Split('/')[1]}";
                        KategoriLogo.SaveAs(Server.MapPath($"~/img/Firma/{filename}"));
                        model.KategoriResim = filename;

                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("", exp.Message);
                    }
                }

                int res = _managerFirmaKategori.Insert(model);
                if (res > 0)
                {
                    return RedirectToAction("KategoriListele", "Firma");
                }
                ModelState.AddModelError("", "Ketegori Eklememedi");
                return RedirectToAction("KategoriEkle", "Firma");
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

            FirmaKategori kat = _managerFirmaKategori.Find(x => x.ID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(kat);
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult KategoriDuzenle(FirmaKategori model, HttpPostedFileBase KategoriLogo)
        {
            ModelState.Remove("KategoriResim");
            if (ModelState.IsValid)
            {
                FirmaKategori kat = _managerFirmaKategori.Find(x => x.ID == model.ID);
                kat.KategoriAdi = model.KategoriAdi;
                kat.KategoriAciklama = model.KategoriAciklama;
                if (KategoriLogo != null &&
                  (KategoriLogo.ContentType == "image/jpeg" ||
                  KategoriLogo.ContentType == "image/jpg" ||
                  KategoriLogo.ContentType == "image/png"))
                {
                    try
                    {
                        if (model.KategoriResim != "default.jpg")
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/img/Firma/" + model.KategoriResim)))
                            {
                                System.IO.File.Delete(Server.MapPath("~/img/Firma/" + model.KategoriResim));
                            }
                        }


                        string filename = $"{AdGetir.ResimAd(model.KategoriAciklama)}.{KategoriLogo.ContentType.Split('/')[1]}";
                        KategoriLogo.SaveAs(Server.MapPath($"~/img/Firma/{filename}"));
                        kat.KategoriResim = filename;

                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("", exp.Message);
                    }
                }
                //else {
                //    kat.KategoriResim = "default.jpg";
                //}

                int res = _managerFirmaKategori.Update(kat);
                if (res > 0)
                {
                    return RedirectToAction("KategoriListele", "Firma");

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

            FirmaKategori frm = _managerFirmaKategori.Find(x => x.ID == id.Value);
            if (frm == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            try
            {
                if (frm.KategoriResim != "default.jpg")
                {
                    if (System.IO.File.Exists(Server.MapPath("~/img/Firma/" + frm.KategoriResim)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/img/Firma/" + frm.KategoriResim));
                    }
                }
            }
            catch { }
            foreach (var item in _managerFirmaKategoriBag.List(x => x.FirmaKategori.ID == id))
            {
                _managerFirmaKategoriBag.Delete(item);
            }
            _managerFirmaKategori.Delete(frm);

           

            return View("KategoriListele", _managerFirmaKategori.List());
        }


        public JsonResult ResimSil(int? id)
        {
            if (id == null)
            {
                return Json(0);
            }
            FirmaResim rsm = _managerFirmaResim.Find(x => x.ID == id);
            if (rsm == null)
            {
                return Json(0);
            }

            int rs = _managerFirmaResim.Delete(rsm);

            if (System.IO.File.Exists(Server.MapPath("~/img/Firma/" + rsm.ResimYol)))
            {
                System.IO.File.Delete(Server.MapPath("~/img/Firma/" + rsm.ResimYol));
            }

            return Json(new { result = rs }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ResimListele(int? id)
        {
            List<FirmaResim> resimlist = _managerFirmaResim.List(x => x.FirmaID == id);

            return PartialView("_PartialResimListele", resimlist);
        }

        public PartialViewResult AnaFirmaKategoriGetir()
        {
            return PartialView("_PartialAnaFirmaKategori", _managerFirmaKategori.List().Take(6).ToList());
        }


        public ActionResult FirmaListele(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //List<Firma> frm = new List<Firma>();
            ViewBag.KategoriID = id;
            List<Firma> frm = _managerFirmaKategoriBag.List(x => x.FirmaKategori.ID == id).Select(x => x.Firma).ToList();
            if (id == 0)
            {
                frm = _managerFirma.List().OrderBy(x=>x.FirmaAdi).ToList();
            }
            if (frm == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            return View(frm);

        }
        public ActionResult DetayGetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Firma kat = _managerFirma.Find(x => x.ID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            return View(kat);
        }
        public PartialViewResult FirmaKategoriDoldur()
        {
            return PartialView("_PartialFirmaMenuGetir", _managerFirmaKategori.List().Take(6).ToList());
        }
        public PartialViewResult FirmaKategoriDoldur1()
        {
            return PartialView("_PartialFooterFirmaMenuGetir", _managerFirmaKategori.List().Take(6).ToList());
        }

        public PartialViewResult FirmaAra(string id)
        {
            string[] data = id.Split(';');
            string harf = data[0];
            int katid = Convert.ToInt32(data[1]);
            List<Firma> firmalar = new List<Firma>();
            if (katid >0)
            {
                firmalar = _managerFirmaKategoriBag.List(x => x.FirmaKategori.ID == katid && x.Firma.FirmaAdi.StartsWith(harf)).Select(x => x.Firma).ToList().OrderBy(x=>x.FirmaAdi).ToList();
                
            }
            else
            {
                firmalar = _managerFirma.List(x => x.FirmaAdi.StartsWith(harf)).OrderBy(x=>x.FirmaAdi).ToList();

            }


            return PartialView("_PartialFirmalariGetir", firmalar);
        }
    }
}