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
    [Table("TOSS_Mevzuat_Kategori")]
    public class MevzuatKategori
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Kategori Adı"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KategoriAdi { get; set; }

        public virtual List<Mevzuat> mevzuatlar { get; set; }
        public MevzuatKategori()
        {
            mevzuatlar = new List<Mevzuat>();
        }
    }
}
