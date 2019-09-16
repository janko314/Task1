using System.Linq;
using System.Web.Mvc;
using Task1.Infrastructure;

namespace Task1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Racun");
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
    }
}