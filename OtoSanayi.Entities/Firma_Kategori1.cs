using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoSanayi.Entities
{
    [Table("TOSS_FirmaKategori")]
    public class Firma_Kategori1
    {
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public virtual FirmaKategori FirmaKategori { get; set; }
        public virtual Firma Firma { get; set; }
    }
}
