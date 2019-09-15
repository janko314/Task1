namespace Task1.Models
{
    public class PregledStavkiRacuna
    {
        public int ID { get; set; }
        public int Kolicina { get; set; }
        public string Proizvod { get; set; }
        public decimal Cena { get; set; }
        public decimal Ukupno { get; set; }
    }

    public class PregledStavkiRacunaView
    {
        public int ID { get; set; }
        public string Kolicina { get; set; }
        public string Proizvod { get; set; }
        public string Cena { get; set; }
        public string Ukupno { get; set; }
    }
}