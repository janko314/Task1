using System.ComponentModel.DataAnnotations;

namespace Task1.Models
{
    public class PregledStavkiRacuna
    {
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0}", ApplyFormatInEditMode = true)]
        public int Kolicina { get; set; }
        public string Proizvod { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.##}", ApplyFormatInEditMode = true)]
        public decimal Cena { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.##}", ApplyFormatInEditMode = true)]
        public decimal Ukupno { get; set; }
    }
}