using System.Linq;
using System.Web.Mvc;
using Task1.Infrastructure;

namespace Task1.Controllers
{
    public class HomeController : Controller
    {
        DataDb _db = new DataDb();

        //public ActionResult Index(string searchTerm = null)
        //{
        //    var model = _db.Racuni.OrderBy(x => x.BrojRacuna)
        //        //.Where(x => searchTerm == null || x.BrojRacuna.StartsWith(searchTerm))
        //        //.Take(10)
        //        //.Select(x => new Data.Racun
        //        //{
        //        //    ID = x.ID,
        //        //    BrojRacuna = x.BrojRacuna,
        //        //    Ukupno = x.Ukupno
        //        //});
        //    return View(model);
        //}

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Pregled računa.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Software developer";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}