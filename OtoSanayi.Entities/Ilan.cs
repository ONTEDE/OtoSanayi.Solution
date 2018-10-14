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
    [Table("TOSS_Ilan")]
    public class Ilan
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
       
        [Required, DisplayName("İlan Başlığı"), StringLength(500, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string Baslik { get; set; }
        [Required, DisplayName("İlan Kısa Açıklama"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KisaAciklama { get; set; }

        [Required, DisplayName("İlan Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("İlan Tarihi")]
        public DateTime IlanTarih { get; set; }
        public int IlanKategoriID { get; set; }
        public int FirmaID { get; set; }
        public virtual Firma firma { get; set; }
        public virtual IlanKategori kategori { get; set; }
        public virtual List<IlanResim> ilanResimler { get; set; }
        public Ilan()
        {
            ilanResimler = new List<IlanResim>();
        }
    }
}
