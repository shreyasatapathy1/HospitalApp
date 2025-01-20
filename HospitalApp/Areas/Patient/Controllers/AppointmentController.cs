using HospitalApp.Data;
using HospitalApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Patient")]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        

        public IActionResult BookAppointment()
        {
            var model = new BookAppointmentViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(BookAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var patient = await _db.Patients.FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (patient == null)
                {
                    ModelState.AddModelError("", "Patient record not found.");
                    return View(model);
                }

                var appointment = new Appointment
                {
                    DoctorId = model.DoctorId,
                    PatientId = patient.Id,
                    Date = DateOnly.FromDateTime(model.Date), // Convert DateTime to DateOnly,
                    TimeSlot = model.TimeSlot,
                    PaymentMethod = model.PaymentMethod,
                    Notes = model.Notes,
                    Status = "Pending",
                    IsPaid = false // Payment processing logic can be added later
                };

                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();

                return RedirectToAction("AppointmentConfirmation");
            }

            return View(model);
        }

        public IActionResult AppointmentConfirmation()
        {
            return View();
        }

        public JsonResult GetDoctorsBySpecialization(string specialization)
        {
            var doctors = _db.Doctors
                             .Where(d => d.Specialty == specialization)
                             .Select(d => new { d.Id, d.User.Name })
                             .ToList();
            return Json(doctors);
        }
    }
}
