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

    [Table("TOSS_Kurumsal_Kategori")]
    public class KurumsalKategori
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Kategori Adı"), StringLength(200, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KategoriAdi { get; set; }

        public virtual List<Kurumsal> kurumsallar { get; set; }
        public KurumsalKategori()
        {
            kurumsallar = new List<Kurumsal>();
        }

    }
}
