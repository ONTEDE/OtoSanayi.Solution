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
    [Table("TOSS_Firma_Resim")]
    public class FirmaResim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DisplayName("Firma Resim"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string ResimYol { get; set; }
        public int FirmaID { get; set; }

        public virtual Firma firma { get; set; }
    }
}
