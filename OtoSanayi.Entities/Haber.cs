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
    [Table("TOSS_Haber")]
    public class Haber
    {
       // StringLength(5000, ErrorMessage = "{0} Alanı{1} Karakter olmalı")

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Haber Başlığı"), StringLength(300, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string HaberBaslik { get; set; }
        [Required, DisplayName("Haber Kısa İçeriği")]
        public string KisaHaberIcerik { get; set; }

        [Required, DisplayName("Haber İçeriği")]
        public string HaberIcerik { get; set; }

        public virtual List<HaberResim> haberResimler { get; set; }
        public Haber()
        {
            haberResimler = new List<HaberResim>();
        }
    }
}
