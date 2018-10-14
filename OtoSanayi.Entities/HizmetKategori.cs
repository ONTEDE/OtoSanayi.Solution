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
    [Table("TOSS_Hizmet_Kategori")]
    public class HizmetKategori
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Kategori Adı"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string HizmetKategoriAdi { get; set; }

        public virtual List<Hizmet> hizmetler { get; set; }

        public HizmetKategori()
        {
            hizmetler = new List<Hizmet>();
        }
    }
}
