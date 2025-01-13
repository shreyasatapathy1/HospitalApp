using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Areas.Patient.Controllers
{
    public class HomeController : Controller
    {
        [Area("Patient")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
