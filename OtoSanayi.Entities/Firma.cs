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
               
        public bool Aktif { get; set; }
        [ DisplayName("Firma Adı")]
        public string FirmaAdi { get; set; }


        [DisplayName("Etiket")]
        public string Etiket { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("SeoName")]
        public string SeoName { get; set; }

        [Required, DisplayName("Firma Yetkili")]
        public string FirmaYetkili { get; set; }

        [DisplayName("Firma Tel")]
        public string FirmaTel { get; set; }
        [DisplayName("Firma Adres(Blok-No)")]
        public string Adres { get; set; }

        [DisplayName("Firma Kısa Açıklama")]
        public string KisaAciklama { get; set; }
        [DisplayName("Firma Açıklama")]
        public string Aciklama { get; set; }

        [DisplayName("Firma Logo")]
        public string Logo { get; set; }
        [DisplayName("Firma WebLink")]
        public string FirmaLink { get; set; }
        //public int FirmaKategoriID { get; set; }
        [DisplayName("Firma Mail")]
        public string FirmaMail{ get; set; }
        [DisplayName("Firma Facebook Adresi")]
        public string FirmaFace { get; set; }
        [DisplayName("Firma Twitter Adresi")]
        public string FirmaTwitter { get; set; }
        [DisplayName("Firma Google Adresi")]
        public string FirmaGoogle { get; set; }
        [DisplayName("Firma İnstagram Adresi")]
        public string FirmaInstagram { get; set; }

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
