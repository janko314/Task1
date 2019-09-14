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

        public ActionResult Autocomplete(string term)
        {
            var model = _db.Racuni.OrderBy(x => x.BrojRacuna)
                .Where(x => x.BrojRacuna.StartsWith(term))
                .Take(10)
                .Select(r => new
                {
                    label = r.BrojRacuna
                });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Racun
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            var model = _db.Racuni.OrderBy(x => x.BrojRacuna)
                .Where(x => searchTerm == null || x.BrojRacuna.StartsWith(searchTerm))
                .ToPagedList(page, 10);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Racuni", model);
            }
            return View(model);
        }

        // GET: Racun/Details/5
        public ActionResult Details(int id)
        {
            //var racun = _db.Racuni.Where(x => x.ID == id).FirstOrDefault();
            var stavke = _db.StavkeRacuna.Where(x => x.RacunId == id).ToList();
            return View(stavke);
        }

        // GET: Racun/Create
        public ActionResult Create()
        {
            //Racun racun = PripremiNoviRacun();
            //return View(racun);
            return RedirectToAction("Create", "StavkaRacuna", new { racunId = 0 });
        }

        // POST: Racun/Create
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

        // GET: Racun/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                //var racun = _db.Racuni.Find(id);
                //var stavke = _db.StavkeRacuna.Where(x => x.Racuni.ID == id).ToList();
                //return View("Index", "StavkaRacuna", stavke);
                return RedirectToAction("Index", "StavkaRacuna", new { racunId = id });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Racun/Edit/5
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

        // GET: Racun/Delete/5
        public ActionResult Delete(int id)
        {
            var racun = _db.Racuni.Where(x => x.ID == id).FirstOrDefault();
            var stavke = _db.StavkeRacuna.Where(x => x.RacunId == id).ToList();
            foreach(StavkaRacuna stavka in stavke)
                _db.StavkeRacuna.Remove(stavka);
            _db.Racuni.Remove(racun);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Racun/Delete/5
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

        //private Racun PripremiNoviRacun()
        //{
        //    int id = 0;
        //    if (_db.Racuni.Count() > 0)
        //        id = _db.Racuni.Max(x => x.ID);
        //    string brojRacuna = String.Format("{0}-{1}", id + 1, DateTime.Now.Year);
        //    return new Racun { BrojRacuna = brojRacuna, Datum = DateTime.Now, Ukupno = 0 };
        //}

    }
}
