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

        // GET: StavkaRacuna
        public ActionResult Index(int? racunId)
        {
            if (racunId == null)
                return RedirectToAction("Index", "Racun");

            Session["racunId"] = racunId;
            var stavke = _db.StavkeRacuna.Where(x => x.RacunId == racunId).ToList();

            List<PregledStavkiRacunaView> model =
                _db.StavkeRacuna.Join(_db.Proizvodi,
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
                .ToList()
                .Select(x => new PregledStavkiRacunaView()
                {
                    ID = x.ID,
                    Kolicina = x.Kolicina.ToString("#,###"),
                    Proizvod = x.Proizvod,
                    Cena = x.Cena.ToString("#,###.00"),
                    Ukupno = (x.Kolicina * x.Cena).ToString("#,###.00")
                })
                .ToList();

            ViewBag.Racun = _db.Racuni.Find(racunId).BrojRacuna;
            return View(model);
        }

        public ActionResult Details(int id)
        {
            return View();
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
                    _db.StavkeRacuna.Add(stavka);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index", new { racunId = stavka.RacunId });
            }
            catch
            {
                return View();
            }
        }

        // GET: StavkaRacuna/Edit/5
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

        // POST: StavkaRacuna/Edit/5
        [HttpPost]
        public ActionResult Edit(StavkaRacuna stavka)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(stavka).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StavkaRacuna/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StavkaRacuna/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
    }
}
