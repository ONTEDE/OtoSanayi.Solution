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
    [Table("TOSS_Kurumsal_Resim")]
    public class KurumsalResim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DisplayName("Resimler"), StringLength(150, ErrorMessage = "{0} Alanı{1} Karakter olmalı")]
        public string ResimYol { get; set; }
        public int KurumsalID { get; set; }

        public virtual Kurumsal kurumsal { get; set; }

    }
}
