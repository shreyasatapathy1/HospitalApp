using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Areas.Admin.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
