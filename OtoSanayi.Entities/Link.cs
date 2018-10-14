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
    [Table("TOSS_Link")]
    public class Link
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Link Adı"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string LinkAdi { get; set; }
        [Required, DisplayName("Link Adres"), StringLength(75, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string LinkAdres { get; set; }
        [DisplayName("Logo"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string LinkLogo { get; set; }
    }
}
