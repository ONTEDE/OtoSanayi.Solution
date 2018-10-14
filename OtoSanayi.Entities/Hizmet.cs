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
    [Table("TOSS_Hizmet")]
    public class Hizmet
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Hizmet"), StringLength(300, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string HizmetBaslik { get; set; }

        [Required, DisplayName("Hizmet Kısa İçeriği"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KisaHizmetIcerik { get; set; }
        [Required, DisplayName("Hizmet İçeriği")]
        public string HizmetIcerik { get; set; }
        public int HizmetKategoriID { get; set; }

        public virtual HizmetKategori kategori { get; set; }
        public virtual List<HizmetResim> hizmetResimler { get; set; }
        public Hizmet()
        {
            hizmetResimler = new List<HizmetResim>();
        }
    }
}
