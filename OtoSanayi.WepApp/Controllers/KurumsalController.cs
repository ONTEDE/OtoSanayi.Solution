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
    public class KurumsalController : Controller
    {

        KurumsalManager _managerkurum = new KurumsalManager();
        KurumsalKategoriManager _managerKurumKategori = new KurumsalKategoriManager();
        KurumsalResimManager _managerKurumResim = new KurumsalResimManager();
        [AuthAdmin]
        public ActionResult Index()
        {

            return View(_managerkurum.ListQueryable().Include(x => x.Kurumsalkategori).Include(c => c.Resimler).OrderByDescending(x => x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
            ViewBag.KurumsalKategoriID = new SelectList(_managerKurumKategori.List(), "ID", "KategoriAdi");

            return View();
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Ekle(Kurumsal model, IEnumerable<HttpPostedFileBase> KurumResimler)
        {
            ViewBag.KurumsalKategoriID = new SelectList(_managerKurumKategori.List(), "ID", "KategoriAdi");
            ModelState.Remove("Resimler");
            if (ModelState.IsValid)
            {
                KurumsalKategori kat = _managerKurumKategori.Find(x => x.ID == model.KurumsalKategoriID);
                model.Kurumsalkategori = kat;

                int res = _managerkurum.Insert(model);

                if (res == 0)
                {
                    ModelState.AddModelError("", "Kurumsal Eklenemedi");

                    return View(model);
                }

                if (KurumResimler != null && KurumResimler.Count() > 0)
                {
                    foreach (HttpPostedFileBase file in KurumResimler)
                    {
                        if (file != null &&
                   (file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png"))
                        {
                            try
                            {
                                KurumsalResim rsm = new KurumsalResim();
                                string filename = $"{AdGetir.ResimAd(model.Baslik)}.{file.ContentType.Split('/')[1]}";
                                file.SaveAs(Server.MapPath($"~/img/Kurum/{filename}"));

                                rsm.ResimYol = filename;
                                rsm.KurumsalID = model.ID;
                                _managerKurumResim.Add(rsm);

                            }
                            catch (Exception exp)
                            {
                                ModelState.AddModelError("", exp.Message);
                            }
                        }

                    }
                    _managerKurumResim.Save();
                }
                return RedirectToAction("Index");

            }
            ViewBag.ID = new SelectList(_managerKurumKategori.List(), "ID", "KategoriAdi");
            return View();
        }
        [AuthAdmin]
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Kurumsal krmsal = _managerkurum.Find(x => x.ID == id.Value);
            if (krmsal == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            ViewBag.ID = new SelectList(_managerKurumKategori.List(), "ID", "KategoriAdi");

            return View(krmsal);
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult Duzenle(Kurumsal model, IEnumerable<HttpPostedFileBase> KurumsalResimler)
        {
            ModelState.Remove("Resimler");
            ViewBag.ID = new SelectList(_managerKurumKategori.List(), "ID", "KategoriAdi");
            if (ModelState.IsValid)
            {
                Kurumsal krmsal = _managerkurum.Find(x => x.ID == model.ID);
                KurumsalKategori kat = _managerKurumKategori.Find(x => x.ID == model.KurumsalKategoriID);

                krmsal.Icerik = model.Icerik;
                krmsal.KisaIcerik = model.KisaIcerik;
                krmsal.Baslik = model.Baslik;
                krmsal.Kurumsalkategori = kat;
                krmsal.KurumsalKategoriID = model.KurumsalKategoriID;
                int res = _managerkurum.Update(krmsal);

                if (res == 0)
                {
                    ModelState.AddModelError("", "Kurumsal Güncellenemedi");
                    ViewBag.ID = new SelectList(_managerKurumKategori.List(), "ID", "KategoriAdi");
                    return View(model);
                }

                if (KurumsalResimler != null && KurumsalResimler.Count() > 0)
                {

                    foreach (HttpPostedFileBase file in KurumsalResimler)
                    {
                        if (file != null &&
                   (file.ContentType == "image/jpeg" ||
                   file.ContentType == "image/jpg" ||
                   file.ContentType == "image/png"))
                        {
                            try
                            {
                                KurumsalResim rsm = new KurumsalResim();
                                string filename = $"{AdGetir.ResimAd(model.Baslik)}.{file.ContentType.Split('/')[1]}";
                                file.SaveAs(Server.MapPath($"~/img/Kurum/{filename}"));

                                rsm.ResimYol = filename;
                                rsm.KurumsalID = krmsal.ID;
                                _managerKurumResim.Add(rsm);

                            }
                            catch (Exception exp)
                            {
                                ModelState.AddModelError("", exp.Message);
                            }
                        }

                    }
                    _managerKurumResim.Save();

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

            Kurumsal krmsal = _managerkurum.Find(x => x.ID == id.Value);
            if (krmsal == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            ViewBag.ID = new SelectList(_managerKurumKategori.List(), "ID", "KategoriAdi");

            return View(krmsal);
        }
        [AuthAdmin]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Kurumsal krmsal = _managerkurum.Find(x => x.ID == id.Value);
            if (krmsal == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            foreach (var item in krmsal.Resimler)
            {
                if (System.IO.File.Exists(Server.MapPath("~/img/Kurum/" + item.ResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath("~/img/Kurum/" + item.ResimYol));
                }
            }
            _managerkurum.Delete(krmsal);

            return RedirectToAction("Index");
        }

        [AuthAdmin]
        public ActionResult KategoriListele()
        {
            return View(_managerKurumKategori.List());
        }
        [AuthAdmin]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult KategoriEkle(KurumsalKategori model)
        {
            if (ModelState.IsValid)
            {
                KurumsalKategori kat = _managerKurumKategori.Find(x => x.KategoriAdi == model.KategoriAdi);
                if (kat != null)
                {
                    ModelState.AddModelError("", "Ketegori Mevcut");
                    return View(model);
                }
                int res = _managerKurumKategori.Insert(model);
                if (res == 0)
                {
                    ModelState.AddModelError("", "Ketegori Eklememedi");
                    return View(model);
                }
                return View("KategoriListele", _managerKurumKategori.List());
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

            KurumsalKategori kat = _managerKurumKategori.Find(x => x.ID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(kat);
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult KategoriDuzenle(KurumsalKategori model)
        {
            if (ModelState.IsValid)
            {
                KurumsalKategori kat = _managerKurumKategori.Find(x => x.ID == model.ID);
                kat.KategoriAdi = model.KategoriAdi;

                int res = _managerKurumKategori.Update(kat);
                if (res > 0)
                {
                    return View("KategoriListele", _managerKurumKategori.List());

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

            KurumsalKategori krmsal = _managerKurumKategori.Find(x => x.ID == id.Value);
            if (krmsal == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            _managerKurumKategori.Delete(krmsal);

            return View("KategoriListele", _managerKurumKategori.List());
        }
        public JsonResult ResimSil(int? id)
        {
            if (id == null)
            {
                return Json(0);
            }
            KurumsalResim rsm = _managerKurumResim.Find(x => x.ID == id);
            if (rsm == null)
            {
                return Json(0);
            }

            int rs = _managerKurumResim.Delete(rsm);

            if (System.IO.File.Exists(Server.MapPath("~/img/Kurum/" + rsm.ResimYol)))
            {
                System.IO.File.Delete(Server.MapPath("~/img/Kurum/" + rsm.ResimYol));
            }

            return Json(new { result = rs }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ResimListele(int? id)
        {
            List<KurumsalResim> resimlist = _managerKurumResim.List(x => x.KurumsalID == id);

            return PartialView("_PartialResimListele", resimlist);
        }
        public PartialViewResult AnaResimGalerisiGetir()
        {
            //int id = _managerKurumKategori.Find(x=>x.KategoriAdi=="RESİMLERLE TOSS").ID;

            return PartialView("_PartialResimGalerisi", _managerKurumResim.List().Where(x => x.kurumsal.Kurumsalkategori.KategoriAdi == "FOTOĞRAFLARLA T.O.S.S").OrderByDescending(x => x.ID).ToList());
        }
        public PartialViewResult KurumsalKategoriDoldur()
        {
            return PartialView("_PartialKurumsalMenuGetir", _managerKurumKategori.List());
        }
        public PartialViewResult KurumsalKategoriDoldur1()
        {
            return PartialView("_PartialKurumsalFooterMenuGetir", _managerKurumKategori.List());
        }
        public PartialViewResult AnaBaskanMesajıGetir()
        {
            return PartialView("_PartialAnaBaskanMesaj",_managerkurum.List().Where(x=>x.Kurumsalkategori.KategoriAdi=="BAŞKANIN MESAJI").OrderByDescending(x=>x.ID).ToList().FirstOrDefault());
        }
        public ActionResult DetayGetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Kurumsal kat = _managerkurum.List(x => x.KurumsalKategoriID == id.Value).OrderByDescending(x => x.ID).FirstOrDefault();
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            return View(kat);
        }
    }
}