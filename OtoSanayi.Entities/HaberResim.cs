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
    [Table("TOSS_Haber_Resim")]
    public class HaberResim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DisplayName("Haber Resmi"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string ResimYol { get; set; }
        public int HaberID { get; set; }

        public virtual Haber haber { get; set; }
    }
}
