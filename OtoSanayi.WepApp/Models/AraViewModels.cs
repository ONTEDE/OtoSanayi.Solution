using OtoSanayi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoSanayi.WepApp.Models
{
    public class AraViewModels
    {
        public List<Firma> firmalist { get; set; }
        public List<Haber> haberlist { get; set; }
        public List<Ilan> ilanlist { get; set; }
        public List<Duyuru> duyurulist { get; set; }
        public AraViewModels()
        {
            firmalist = new List<Firma>();
            haberlist = new List<Haber>();
            ilanlist = new List<Ilan>();
            duyurulist = new List<Duyuru>();
        }
    }
}