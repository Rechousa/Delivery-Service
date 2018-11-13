using System.Configuration;
using System.Web.Mvc;

namespace DeliveryServiceExercise.Web.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            ViewData["apiURL"] = ConfigurationManager.AppSettings.Get("DeliveryServiceAPIUrl");
            return View();
        }
    }
}