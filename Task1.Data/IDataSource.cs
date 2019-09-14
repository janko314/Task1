using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1.Data
{
    public interface IDataSource
    {
        IQueryable<Racun> Racuni { get; }
        IQueryable<Proizvod> Proizvodi { get; }
        IQueryable<StavkaRacuna> StavkeRacuna { get; }
    }
}
