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
    [Table("TOSS_Slider")]
    public class Slider
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, DisplayName("Slider Baslik"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string SliderBaslik { get; set; }

        [Required, DisplayName("Slider Link"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string SliderLink { get; set; }

        [DisplayName("Resim"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string SliderYol { get; set; }
    }
}
