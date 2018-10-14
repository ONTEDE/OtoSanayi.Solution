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
    [Table("TOSS_Hizmet_Resim")]
    public class HizmetResim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Hizmet Resimleri"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string ResimYol { get; set; }
        public int HizmetID { get; set; }

        public virtual Hizmet hizmet { get; set; }
    }
}
