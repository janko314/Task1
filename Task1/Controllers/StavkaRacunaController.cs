using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Task1.Data;
using Task1.Infrastructure;
using Task1.Models;

namespace Task1.Controllers
{
    public class StavkaRacunaController : Controller
    {
        DataDb _db = new DataDb();

        //[OutputCache(CacheProfile = "Long", VaryByHeader = "X-Request-With", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index(int? racunId)
        {
            if (racunId == null)
                return RedirectToAction("Index", "Racun");

            Session["racunId"] = racunId;

            List<PregledStavkiRacuna> model =
                _db.StavkeRacuna
                .Where(x => x.RacunId == racunId)
                .Join(_db.Proizvodi,
                                      stavka => stavka.ProizvodId,
                                      proizvod => proizvod.ID,
                                      (stavka, proizvod) => new PregledStavkiRacuna()
                                      {
                                          ID = stavka.ID,
                                          Kolicina = stavka.Kolicina,
                                          Proizvod = proizvod.Naziv,
                                          Cena = proizvod.Cena,
                                          Ukupno = (stavka.Kolicina * proizvod.Cena)
                                      })
                .ToList();

            ViewBag.Racun = _db.Racuni.Find(racunId).BrojRacuna;
            return View(model);
        }

        public ActionResult Create()
        {
            var racun = _db.Racuni.Find(Session["racunId"]);
            List<SelectListItem> ListaProizvoda = PripremiListuProizvoda();
            StavkaRacuna stavka = new StavkaRacuna();
            stavka.ListaProizvoda = ListaProizvoda;
            stavka.RacunId = racun.ID;
            return View(stavka);
        }

        [HttpPost]
        public ActionResult Create(StavkaRacuna stavka)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    stavka.ListaProizvoda = PripremiListuProizvoda();                    
                    if(_db.StavkeRacuna.Where(x => x.ProizvodId == stavka.ProizvodId).FirstOrDefault() == null)
                        _db.StavkeRacuna.Add(stavka);
                    else
                        _db.StavkeRacuna.Where(x => x.ProizvodId == stavka.ProizvodId).FirstOrDefault().Kolicina += stavka.Kolicina;
                    _db.SaveChanges();
                    _db.Racuni.Find(stavka.RacunId).Ukupno = VrednostRacuna(stavka.RacunId);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index", new { racunId = stavka.RacunId });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int? Id)
        {
            if (Id != null)
            {
                List<SelectListItem> ListaProizvoda = PripremiListuProizvoda();
                var stavka = _db.StavkeRacuna.Find(Id);
                stavka.ListaProizvoda = ListaProizvoda;
                return View(stavka);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(StavkaRacuna stavka)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    _db.Entry(stavka).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                    _db.Racuni.Find(stavka.RacunId).Ukupno = VrednostRacuna(stavka.RacunId);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index", "StavkaRacuna", new { racunId = stavka.RacunId });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            int rId = (int)Session["racunId"];
            var stavka = _db.StavkeRacuna.Find(id);
            _db.StavkeRacuna.Remove(stavka);
            _db.SaveChanges();
            _db.Racuni.Find(stavka.RacunId).Ukupno = VrednostRacuna(rId);
            _db.SaveChanges();
            return RedirectToAction("Index", "StavkaRacuna", new { racunId = rId });
        }

        private List<SelectListItem> PripremiListuProizvoda()
        {
            var artikli = _db.Proizvodi.ToList();

            List<SelectListItem> ListaProizvoda = new List<SelectListItem>();
            foreach (var item in artikli)
            {
                ListaProizvoda.Add(new SelectListItem
                {
                    Text = item.Naziv,
                    Value = item.ID.ToString()
                });
            }

            return ListaProizvoda;
        }

        private decimal VrednostRacuna(StavkaRacuna stavka)
        {
            decimal ukupno = 0;
            IQueryable<StavkaRacuna> stavke = _db.StavkeRacuna.Where(z => z.RacunId == stavka.RacunId);

            if (stavke.Count() > 0)
                ukupno = stavke.Sum(x => x.Kolicina * _db.Proizvodi.Where(a=>a.ID == x.ProizvodId).FirstOrDefault().Cena);
            ukupno += stavka.Kolicina * _db.Proizvodi.Find(stavka.ProizvodId).Cena;
            return ukupno;
        }
        private decimal VrednostRacuna(int racunId)
        {
            decimal ukupno = 0;
            IQueryable<StavkaRacuna> stavke = _db.StavkeRacuna.Where(z => z.RacunId == racunId);
            if (stavke.Count() > 0)
                ukupno = stavke.Sum(x => x.Kolicina * _db.Proizvodi.Where(a => a.ID == x.ProizvodId).FirstOrDefault().Cena);
            return ukupno;
        }

    }
}
