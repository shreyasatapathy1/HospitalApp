using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewDoctors()
        {
            List<Models.Doctor> doctorList = _db.Doctors.ToList();
            return View(doctorList);
        }
        public IActionResult AddDoctor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDoctor(Models.Doctor obj)
        {
            _db.Doctors.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("ViewDoctors");

        }
    }
}
