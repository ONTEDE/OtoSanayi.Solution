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
    [Table("TOSS_Mevzuat")]
    public class Mevzuat
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Mevzuat Adı"), StringLength(250, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string Adi { get; set; }
        [Required, DisplayName("Mevzuat Link"), StringLength(250, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string Mevzuatlink { get; set; }
        public int MevzuatKategoriID { get; set; }

        public virtual MevzuatKategori kategori { get; set; }
    }
}
