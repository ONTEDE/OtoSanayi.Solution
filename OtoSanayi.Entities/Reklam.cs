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
    [Table("TOSS_Reklam")]
    public class Reklam
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("Reklam Çeşidi")]
        public int ReklamCesit { get; set; }

        [DisplayName("Reklam Adı"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string ReklamAdi { get; set; }

        [DisplayName("Reklam link"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string ReklamLink { get; set; }
        [DisplayName("Reklam Resim"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string ResimYol { get; set; }
        
    }
}
