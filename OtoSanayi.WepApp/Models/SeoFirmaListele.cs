using OtoSanayi.DataAccessLayer;
using OtoSanayi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoSanayi.WepApp.Models
{
    public class SeoFirmaListele
    {
        FirmaManager fm = new FirmaManager();

        public List<Firma> firmaGetir() {

            return fm.List();

        }

    }
}