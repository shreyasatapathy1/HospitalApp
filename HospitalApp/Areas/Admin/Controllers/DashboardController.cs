using HospitalApp.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public DashboardController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public IActionResult Index()
        {
            var appointments = _appointmentRepository.GetAllAppointmentsWithDetails();
            return View(appointments);
        }
    }
}

