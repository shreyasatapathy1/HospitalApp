using HospitalApp.Data.Repository;
using HospitalApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApp.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles = "Doctor")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository, UserManager<IdentityUser> userManager, IDoctorRepository doctorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _userManager = userManager;
            _doctorRepository = doctorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var doctor = _doctorRepository.Find(d => d.UserId == user.Id).FirstOrDefault();

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            var appointments = _appointmentRepository.GetAppointmentsByDoctorId(doctor.Id);
            var viewModel = new DoctorAppointmentsViewModel
            {
                DoctorName = user.UserName,
                UpcomingAppointments = appointments.ToList()
            };

            return View(viewModel);
        }
    }
}
