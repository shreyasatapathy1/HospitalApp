using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Areas.Doctor.Controllers
{
    public class HomeController : Controller
    {
        [Area("Doctor")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
