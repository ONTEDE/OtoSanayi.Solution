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
    [Table("TOSS_Duyuru")]
    public class Duyuru
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required,DisplayName("Duyuru Başlığı"),StringLength(150,ErrorMessage ="{0} Alanı{1} Karakter olmalı")]
        public string DuyuruBaslik { get; set; }
        [Required, DisplayName("Duyuru İçeriği"), StringLength(300, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string KisaDuyuruIcerik { get; set; }
        [Required, DisplayName("Duyuru İçeriği")]
        public string DuyuruIcerik { get; set; }
        [DisplayName("Duyuru Tarihi")]
        public DateTime DuyuruTarih { get; set; }
    }
}
