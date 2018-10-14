using OtoSanayi.BusinessLayer;
using OtoSanayi.Entities;
using OtoSanayi.WepApp.Filter;
using OtoSanayi.WepApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OtoSanayi.WepApp.Controllers
{
    public class LinkController : Controller
    {
        LinkManager _managerLink = new LinkManager();
        // GET: Link
        [AuthAdmin]
        public ActionResult Index()
        {
            return View(_managerLink.ListQueryable().OrderByDescending(x => x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
            return View();
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Ekle(Link model,HttpPostedFileBase Logo)
        {
            ModelState.Remove("LinkLogo");
            if (ModelState.IsValid)
            {
                if (Logo != null &&
                   (Logo.ContentType == "image/jpeg" ||
                   Logo.ContentType == "image/jpg" ||
                   Logo.ContentType == "image/png"))
                {
                    //string filename = $"{model.LinkAdi}.{Logo.ContentType.Split('/')[1]}";
                    //Logo.SaveAs(Server.MapPath($"~/img/LinkLogo/{filename}"));
                   string filename=ResimKayit(Logo, "LinkLogo",model.LinkAdi);
                    model.LinkLogo = filename;
                }
                else
                {
                    ModelState.AddModelError("", "Resim Formatı JPG veya PNG olmalıdır");
                    return View(model);
                }
                var result = _managerLink.Insert(model);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Kategori Ekleme İşlemi Başarısız oldu");
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

            Link kat = _managerLink.Find(x => x.ID == id.Value);
            if (kat == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(kat);
        }

        [HttpPost]
        [AuthAdmin]
        public ActionResult Duzenle(Link model,HttpPostedFileBase Logo)
        {
            var kat = _managerLink.Find(x => x.ID == model.ID);
            if (ModelState.IsValid)
            {
                if (Logo != null &&
                    (Logo.ContentType == "image/jpeg" ||
                    Logo.ContentType == "image/jpg" ||
                    Logo.ContentType == "image/png"))
                {
                    if (System.IO.File.Exists(Server.MapPath("~/img/LinkLogo/" + kat.LinkLogo)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/img/LinkLogo/" + kat.LinkLogo));
                    }

                    string filename = ResimKayit(Logo,"LinkLogo",model.LinkAdi);
                    kat.LinkLogo = filename;

                }

                kat.LinkAdi = model.LinkAdi;
                kat.LinkAdres = model.LinkAdres;

                _managerLink.Update(kat);


                return RedirectToAction("Index");
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

            Link lnk = _managerLink.Find(x => x.ID == id.Value);
            if (lnk == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            if (System.IO.File.Exists(Server.MapPath("~/img/LinkLogo/" + lnk.LinkLogo)))
            {
                System.IO.File.Delete(Server.MapPath("~/img/LinkLogo/" + lnk.LinkLogo));
            }
            _managerLink.Delete(lnk);
            return RedirectToAction("Index");
        }
        [AuthAdmin]
        public string ResimKayit(HttpPostedFileBase resim,string ResimYol,string ResimAdi)
        {
            string yeniad = AdGetir.ResimAd(ResimAdi) + Path.GetExtension(resim.FileName);
            Image orj = Image.FromStream(resim.InputStream);

            Bitmap kucukresim = new Bitmap(orj, 60, 50);
            //Bitmap buyukresim = new Bitmap(orj, 600, 400);

            kucukresim.Save(Server.MapPath($"/img/{ResimYol}/" + yeniad));
            //buyukresim.Save(Server.MapPath("/Content/images/Photos/big/" + yeniad));

            return yeniad;
        }
       
        public ActionResult LinkListele()
		{
			return View(_managerLink.List().OrderByDescending(x=>x.ID).ToList());
		}

    }
}
