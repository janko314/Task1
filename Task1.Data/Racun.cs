using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task1.Data
{
    public class Racun
    {
        [Key]
        public virtual int ID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Broj računa")]
        public virtual string BrojRacuna { get; set; }

        [Required]
        public virtual DateTime Datum { get; set; }
        [Required]
        public virtual Decimal Ukupno { get; set; }

        public virtual ICollection<StavkaRacuna> Stavke { get; set; }
    }
}