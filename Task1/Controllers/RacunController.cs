using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using Task1.Data;
using Task1.Infrastructure;

namespace Task1.Controllers
{
    public class RacunController : Controller
    {
        DataDb _db = new DataDb();

        //[OutputCache(CacheProfile="Long", VaryByHeader="X-Request-With", Location=System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            var model = _db.Racuni.OrderBy(x => x.BrojRacuna).ToList();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            return RedirectToAction("Index", "StavkaRacuna", new { racunId = id });
        }

        public ActionResult Create()
        {
            Racun racun = PripremiNoviRacun();
            return View(racun);
        }

        [HttpPost]
        public ActionResult Create(Racun Racuni)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Racun racun = PripremiNoviRacun(Racuni);
                    _db.Racuni.Add(racun);
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id != null)
            {
                var racun = _db.Racuni.Find(Id);
                return View(racun);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Racun racun)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(racun).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var racun = _db.Racuni.Where(x => x.ID == id).FirstOrDefault();
            var stavke = _db.StavkeRacuna.Where(x => x.RacunId == id).ToList();
            foreach(StavkaRacuna stavka in stavke)
                _db.StavkeRacuna.Remove(stavka);
            _db.Racuni.Remove(racun);
            _db.SaveChanges();
            return RedirectToAction("Index", "Racun");
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private Racun PripremiNoviRacun(Racun racun=null)
        {
            int id = 0;
            if (_db.Racuni.Count() > 0)
                id = _db.Racuni.Max(x => x.ID);
            string brojRacuna = String.Format("{0}-{1}", id + 1, DateTime.Now.Year);
            if(racun == null)
                return new Racun { BrojRacuna = brojRacuna, Datum = DateTime.Now, Ukupno = 0 };
            else
                return new Racun { BrojRacuna = brojRacuna, Datum = DateTime.Now, Ukupno = racun.Ukupno };
        }
    }
}
