using OtoSanayi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoSanayi.WepApp.Models
{
    public class KategoriFirmaViewModel
    {
        public List<FirmaKategori> FirmaKategoriler { get; set; }
        public Firma Firma { get; set; }
    }
}