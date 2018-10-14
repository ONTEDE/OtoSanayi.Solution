using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OtoSanayi.Entities
{
    [Table("TOSS_Firma")]
    public class Firma
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Firma Adı"), StringLength(75, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string FirmaAdi { get; set; }
        [Required, DisplayName("Firma Tel"), StringLength(11, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string FirmaTel { get; set; }
        [Required, DisplayName("Firma Adres(Blok-No)"), StringLength(300, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string Adres { get; set; }

        [DisplayName("Firma Kısa Açıklama"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KisaAciklama { get; set; }
        [DisplayName("Firma Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Firma Logo"), StringLength(100, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string Logo { get; set; }
        [DisplayName("Firma WebLink"), StringLength(75, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string FirmaLink { get; set; }
        //public int FirmaKategoriID { get; set; }

        public virtual List<FirmaResim> FirmaResimler { get; set; }
        public virtual List<Ilan> FirmaIlanlar { get; set; }
        //public virtual FirmaKategori Kategori { get; set; }
        public virtual List<Firma_Kategori1> KategoriFirma { get; set; }
        public Firma()
        {
            FirmaResimler = new List<FirmaResim>();
            FirmaIlanlar = new List<Ilan>();
            KategoriFirma = new List<Firma_Kategori1>();
        }


    }
}
