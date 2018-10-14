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
    [Table("TOSS_Kurumsal")]
    public class Kurumsal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Baslik"), StringLength(500, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string Baslik { get; set; }
        [Required, DisplayName("Kısa İçerik"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KisaIcerik { get; set; }

        [Required, DisplayName("İçerik")]
        public string Icerik { get; set; }
        public int KurumsalKategoriID { get; set; }

        public virtual KurumsalKategori Kurumsalkategori { get; set; }
        public virtual List<KurumsalResim> Resimler { get; set; }

        public Kurumsal()
        {
            Resimler = new List<KurumsalResim>();
        }
    }
}
