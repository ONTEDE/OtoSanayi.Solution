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
    [Table("TOSS_Kullanici")]

    public class Kullanici
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Adı"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string Adi { get; set; }
        [Required, DisplayName("Soyadı"), StringLength(75, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string Soyadi { get; set; }
        [Required, DisplayName("Kullanıcı Adı"), StringLength(75, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KullaniciAdi { get; set; }
        [Required, DisplayName("Parola"), StringLength(75, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string Parola { get; set; }
    }
}
