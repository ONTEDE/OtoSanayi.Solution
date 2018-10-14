using OtoSanayi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoSanayi.WepApp.Models
{
    public class ViewIlan
    {
        public List<Ilan> Ilanlar { get; set; }
        public List<IlanKategori> Kategoriler { get; set; }
        public ViewIlan()
        {
            Ilanlar = new List<Ilan>();
            Kategoriler = new List<IlanKategori>();
        }
    }
}