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
    [Table("TOSS_Ilan_Resim")]
    public class IlanResim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Ilan Resimleri"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string ResimYol { get; set; }
        public int IlanID { get; set; }

        public virtual Ilan ilan { get; set; }
    }
}
