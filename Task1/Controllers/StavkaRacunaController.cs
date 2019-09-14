using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Task1.Data;
using Task1.Infrastructure;

namespace Task1.Controllers
{
    public class StavkaRacunaController : Controller
    {
        DataDb _db = new DataDb();

        // GET: StavkaRacuna
        public ActionResult Index(int racunId)
        {
            var model = _db.StavkeRacuna.Where(x => x.RacunId == racunId).ToList();
            return View(model);
        }

        // GET: StavkaRacuna/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StavkaRacuna/Create
        public ActionResult Create()
        {
            var artikli = _db.Proizvodi.ToList();

            List<SelectListItem> ListaArtikala = new List<SelectListItem>();
            foreach (var item in artikli)
            {
                ListaArtikala.Add(new SelectListItem
                {
                    Text = item.Naziv,
                    Value = item.ID.ToString()
                });
            }
            ViewBag.Artikli = ListaArtikala;
            return View();
        }

        // POST: StavkaRacuna/Create
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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StavkaRacuna/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StavkaRacuna/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
    }
}
