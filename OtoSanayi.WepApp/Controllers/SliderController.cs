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
    public class SliderController : Controller
    {
        // GET: Slider
        SliderManager _managerSlider = new SliderManager();
        // GET: Link
        [AuthAdmin]
        public ActionResult Index()
        {
            return View(_managerSlider.List().OrderByDescending(x=>x.ID).ToList());
        }
        [AuthAdmin]
        public ActionResult Ekle()
        {
            return View();
        }
        [AuthAdmin]
        [HttpPost]
        public ActionResult Ekle(Slider model, HttpPostedFileBase SliderResim)
        {
            ModelState.Remove("SliderYol");
            if (ModelState.IsValid)
            {
                if (SliderResim != null &&
                   (SliderResim.ContentType == "image/jpeg" ||
                   SliderResim.ContentType == "image/jpg" ||
                   SliderResim.ContentType == "image/png"))
                {
                    string filename = $"{AdGetir.ResimAd(model.SliderBaslik)}.{SliderResim.ContentType.Split('/')[1]}";
                    SliderResim.SaveAs(Server.MapPath($"~/img/Slider/{filename}"));
                    //string filename = ResimKayit(SliderResim, "LinkLogo", model.LinkAdi);
                    model.SliderYol = filename;
                }
                else
                {
                    ModelState.AddModelError("", "Resim Formatı JPG veya PNG olmalıdır");
                    return View(model);
                }
                var result = _managerSlider.Insert(model);
                if (result == 0)
                {
                    ModelState.AddModelError("", "Slider Ekleme İşlemi Başarısız oldu");
                    return View(model);
                }

                return RedirectToAction("Index");
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

            Slider slider = _managerSlider.Find(x => x.ID == id.Value);
            if (slider == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            return View(slider);
        }

        [HttpPost]
        [AuthAdmin]
        public ActionResult Duzenle(Slider model, HttpPostedFileBase SliderResim)
        {
            var slider = _managerSlider.Find(x => x.ID == model.ID);
            if (ModelState.IsValid)
            {
                if (SliderResim != null &&
                    (SliderResim.ContentType == "image/jpeg" ||
                    SliderResim.ContentType == "image/jpg" ||
                    SliderResim.ContentType == "image/png"))
                {
                    if (System.IO.File.Exists(Server.MapPath("~/img/Slider/" + slider.SliderYol)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/img/Slider/" + slider.SliderYol));
                    }

                    string filename = $"{AdGetir.ResimAd(model.SliderBaslik)}.{SliderResim.ContentType.Split('/')[1]}";
                    SliderResim.SaveAs(Server.MapPath($"~/img/Slider/{filename}"));
                    //string filename = ResimKayit(SliderResim, "LinkLogo", model.LinkAdi);
                    slider.SliderYol = filename;

                }

                slider.SliderBaslik = model.SliderBaslik;
                slider.SliderLink = model.SliderLink;

                _managerSlider.Update(slider);


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

            Slider slider = _managerSlider.Find(x => x.ID == id.Value);
            if (slider == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            if (System.IO.File.Exists(Server.MapPath("~/img/Slider/" + slider.SliderYol)))
            {
                System.IO.File.Delete(Server.MapPath("~/img/Slider/" + slider.SliderYol));
            }
            _managerSlider.Delete(slider);
            return RedirectToAction("Index");
        }
      
        public PartialViewResult AnaSliderGetir()
        {

            return PartialView("_PartialAnaSliderGetir", _managerSlider.List().OrderByDescending(x=>x.ID).Take(5).ToList());
        }

    }
}