using HospitalApp.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApp.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Patient")]
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;

        public DashboardController(UserManager<IdentityUser> userManager, IAppointmentRepository appointmentRepository, IPatientRepository patientRepository)
        {
            _userManager = userManager;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = _patientRepository.Find(p => p.UserId == user.Id).FirstOrDefault();

            if (patient == null)
            {
                return NotFound("Patient record not found.");
            }

            var appointments = _appointmentRepository.GetAppointmentsByPatientId(patient.Id);
            if (appointments == null)
            {
                return View(null);
            }
            return View(appointments);
        }

    }
}
