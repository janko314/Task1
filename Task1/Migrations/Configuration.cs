namespace Task1.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Task1.Infrastructure.DataDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Task1.Infrastructure.DataDb context)
        {
            context.Proizvodi.AddOrUpdate(d => d.Naziv,
                new Data.Proizvod { Naziv = "Guma", Cena = 12.45m },
                new Data.Proizvod { Naziv = "Felna", Cena = 56.67m },
                new Data.Proizvod { Naziv = "Retrovizor", Cena = 23.89m },
                new Data.Proizvod { Naziv = "Vetrobran prednji", Cena = 518.01m },
                new Data.Proizvod { Naziv = "Vetrobran zadnji", Cena = 445.51m },
                new Data.Proizvod { Naziv = "Auspuh", Cena = 142.59m }
                ); ;
        }
    }
}
