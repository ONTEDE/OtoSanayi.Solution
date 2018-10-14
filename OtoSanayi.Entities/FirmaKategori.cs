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
    [Table("TOSS_Firma_Kategori")]
    public class FirmaKategori
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Kategori Adı"), StringLength(75, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KategoriAdi { get; set; }

        [Required, DisplayName("Kategori Aciklama"), StringLength(300, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KategoriAciklama { get; set; }

        [DisplayName("Seç")]
        public bool Secili { get; set; }

        [DisplayName("Kategori Resim"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KategoriResim { get; set; }

        
        //public virtual List<Firma> firmalar { get; set; }
        public virtual List<Firma_Kategori1> KategoriFirma { get; set; }

        public FirmaKategori()
        {
            //firmalar = new List<Firma>();
            KategoriFirma = new List<Firma_Kategori1>();
        }
    }
}
