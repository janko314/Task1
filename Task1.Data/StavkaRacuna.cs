using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task1.Data
{
    public class StavkaRacuna
    {
        [Key]
        public virtual int ID { get; set; }
        [Required]
        public virtual int Kolicina { get; set; }

        public virtual int RacunId { get; set; }

        public virtual int ProizvodId { get; set; }        
    }
}