using HospitalApp.Data.Repository;
using HospitalApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
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
