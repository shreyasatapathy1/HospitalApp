using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Areas.Doctor.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Doctor")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
