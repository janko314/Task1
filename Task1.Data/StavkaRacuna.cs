using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Task1.Data
{
    public class StavkaRacuna
    {
        [Key]
        public virtual int ID { get; set; }
        [Required]
        public virtual int Kolicina { get; set; }

        public virtual int RacunId { get; set; }

        [Display(Name = "Proizvod")]
        public virtual int ProizvodId { get; set; }

        [NotMapped]
        public List<SelectListItem> ListaProizvoda { get; set; }
    }
}