using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task1.Data
{
    public class Proizvod
    {
        [Key]
        public virtual int ID { get; set; }
        [Required]
        [StringLength(30)]
        public virtual string Naziv { get; set; }
        [Required]
        public virtual decimal Cena { get; set; }
        public virtual ICollection<StavkaRacuna> Stavke { get; set; }
    }
}