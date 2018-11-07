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
    [ValidateInput(false)]
    public class MevzuatController : Controller
    {
        MevzuatManager _managerMevzuat = new MevzuatManager();
        MevzuatKategoriManager _managerMevzuatKategori = new MevzuatKategoriManager();
        [AuthAdmin]
        public ActionResult Index()
        {

            return View(_managerMevzuat.ListQueryable().OrderByDescending(x => x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
            Mevzuat m = new Mevzuat();
            ViewBag.MevzuatKategoriID = new SelectList(_managerMevzuatKategori.List(), "ID", "KategoriAdi");

            return View();
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Ekle(Mevzuat model)
        {
            if (ModelState.IsValid)
            {
                MevzuatKategori kat = _managerMevzuatKategori.Find(x => x.ID == model.MevzuatKategoriID);
                model.kategori = kat;

                int res = _managerMevzuat.Insert(model);

                if (res == 0)
                {
                    ModelState.AddModelError("", "Mevzuat Eklenemedi");
                    ViewBag.MevzuatKategoriID = new SelectList(_managerMevzuatKategori.List(), "ID", "KategoriAdi");
                    return View(model);
                }
               
                return RedirectToAction("Index");

            }
            ViewBag.MevzuatKategoriID = new SelectList(_managerMevzuatKategori.List(), "ID", "KategoriAdi");
            return View();
        }
        [AuthAdmin]
        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mevzuat mvzuat = _managerMevzuat.Find(x => x.ID == id.Value);
            if (mvzuat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }


            ViewBag.ID = new SelectList(_managerMevzuatKategori.List(), "ID", "KategoriAdi");

            return View(mvzuat);
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult Duzenle(Mevzuat model)
        {
            
            if (ModelState.IsValid)
            {
                Mevzuat mvzuat = _managerMevzuat.Find(x => x.ID == model.ID);
                MevzuatKategori kat = _managerMevzuatKategori.Find(x => x.ID == model.MevzuatKategoriID);


                mvzuat.Adi = model.Adi;
                mvzuat.Mevzuatlink = model.Mevzuatlink;
                mvzuat.MevzuatKategoriID = kat.ID;
                mvzuat.kategori = kat;
                int res = _managerMevzuat.Update(mvzuat);

                if (res == 0)
                {
                    ModelState.AddModelError("", "Hizmet Güncellenemedi");
                    ViewBag.ID = new SelectList(_managerMevzuatKategori.List(), "ID", "KategoriAdi");
                    return View(model);
                }
               
                return RedirectToAction("Index");


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

            Mevzuat mvzuat = _managerMevzuat.Find(x => x.ID == id.Value);
            if (mvzuat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
           
            _managerMevzuat.Delete(mvzuat);

            return RedirectToAction("Index");
        }
        [AuthAdmin]
        public ActionResult KategoriListele()
        {
            return View(_managerMevzuatKategori.List());
        }
        [AuthAdmin]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        [AuthAdmin]
        public ActionResult KategoriEkle(MevzuatKategori model)
        {
            if (ModelState.IsValid)
            {
                MevzuatKategori kat = _managerMevzuatKategori.Find(x => x.KategoriAdi == model.KategoriAdi);
                if (kat != null)
                {
                    ModelState.AddModelError("", "Ketegori Mevcut");
                    return View(model);
                }
                int res = _managerMevzuatKategori.Insert(model);
                if (res == 0)
                {
                    ModelState.AddModelError("", "Ketegori Eklememedi");
                    return View(model);
                }
                return View("KategoriListele", _managerMevzuatKategori.List());
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

            MevzuatKategori kat = _managerMevzuatKategori.Find(x => x.ID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(kat);
        }

        [HttpPost]
        [AuthAdmin]
        public ActionResult KategoriDuzenle(MevzuatKategori model)
        {
            if (ModelState.IsValid)
            {
                MevzuatKategori kat = _managerMevzuatKategori.Find(x => x.ID == model.ID);
                kat.KategoriAdi = model.KategoriAdi;

                int res = _managerMevzuatKategori.Update(kat);
                if (res > 0)
                {
                    return View("KategoriListele", _managerMevzuatKategori.List());

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

            MevzuatKategori kat = _managerMevzuatKategori.Find(x => x.ID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            _managerMevzuatKategori.Delete(kat);

            return View("KategoriListele", _managerMevzuatKategori.List());
        }

        public PartialViewResult MevzuatKategoriDoldur()
        {
            return PartialView("_PartialMevzuatMenuGetir",_managerMevzuatKategori.List());
        }
        public ActionResult DetayGetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Mevzuat> kat = _managerMevzuat.List(x => x.MevzuatKategoriID == id.Value).OrderByDescending(x => x.ID).ToList();
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            return View(kat);
        }
    }
}