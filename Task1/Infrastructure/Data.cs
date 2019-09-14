using System.Data.Entity;
using System.Linq;
using Task1.Data;

namespace Task1.Infrastructure
{
    public class DataDb : DbContext, IDataSource
    {
        public DataDb() : base("name=DefaultConnection")
        {
        }

        public DbSet<Racun> Racuni { get; set; }
        public DbSet<StavkaRacuna> StavkeRacuna { get; set; }
        public DbSet<Proizvod> Proizvodi { get; set; }

        IQueryable<Racun> IDataSource.Racuni
        {
            get { return Racuni; }
        }

        IQueryable<Proizvod> IDataSource.Proizvodi
        {
            get { return Proizvodi; }
        }

        IQueryable<StavkaRacuna> IDataSource.StavkeRacuna
        {
            get { return StavkeRacuna; }
        }
    }
}