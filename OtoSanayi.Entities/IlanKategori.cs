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
    [Table("TOSS_Ilan_Kategori")]
    public class IlanKategori
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IlanKategoriID { get; set; }
        [Required, DisplayName("KategoriAdi"), StringLength(75, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KategoriAdi { get; set; }

        public virtual List<Ilan> Ilanlar { get; set; }
        public IlanKategori()
        {
            Ilanlar = new List<Ilan>();
        }

    }
}
