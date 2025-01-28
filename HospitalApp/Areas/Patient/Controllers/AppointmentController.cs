
using HospitalApp.Data.Repository;
using HospitalApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HospitalApp.Models;

namespace HospitalApp.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Patient")]
    public class AppointmentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicalReportRepository _medicalReportRepository;

        public AppointmentController(UserManager<IdentityUser> userManager, IMedicalReportRepository medicalReportRepository, IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository, IPatientRepository patientRepository)
        {
            _userManager = userManager;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _medicalReportRepository = medicalReportRepository;
        }

        public IActionResult BookAppointment()
        {
            var model = new BookAppointmentViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(BookAppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                ModelState.AddModelError("", "Please correct the errors in the form.");
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(model);
            }

            Console.WriteLine($"User ID: {user.Id}");

            var patient = _patientRepository.Find(p => p.UserId == user.Id).FirstOrDefault();
            if (patient == null)
            {
                ModelState.AddModelError("", "Patient record not found.");
                return View(model);
            }

            Console.WriteLine($"Patient ID: {patient.Id}");

            if (_appointmentRepository.IsSlotAvailable(model.DoctorId, DateOnly.FromDateTime(model.Date), model.TimeSlot))
            {
                var appointment = new Appointment
                {
                    DoctorId = model.DoctorId,
                    PatientId = patient.Id,
                    Date = DateOnly.FromDateTime(model.Date),
                    TimeSlot = model.TimeSlot,
                    PaymentMethod = model.PaymentMethod,
                    Notes = model.Notes,
                    Status = "Confirmed",
                    IsPaid = false
                };

                _appointmentRepository.Add(appointment);

                Console.WriteLine("Appointment booked successfully!");

                return RedirectToAction("AppointmentConfirmation");
            }
            else
            {
                ModelState.AddModelError("", "Selected time slot is not available. Please choose another slot.");
                return View(model);
            }
        }

        public IActionResult AppointmentConfirmation()
        {
            return View();
        }

        [HttpGet]
        public JsonResult CheckSlotAvailability(int doctorId, DateTime date, string timeSlot)
        {
            bool isAvailable = _appointmentRepository.IsSlotAvailable(doctorId, DateOnly.FromDateTime(date), timeSlot);
            return Json(new { isAvailable });
        }

        public JsonResult GetDoctorsBySpecialization(string specialization)
        {
            var doctors = _doctorRepository.FindBySpecialization(specialization)
                           .Select(d => new
                           {
                               d.Id,
                               Name = d.User != null ? d.User.Name : "No Name Available"
                           })
                           .ToList();

            return Json(doctors);
        }

        public IActionResult ViewReport(int appointmentId)
        {
            //var report = _medicalReportRepository.Find(r => r.AppointmentId == appointmentId)
            //                                     .FirstOrDefault();

            var report = _medicalReportRepository.GetReportWithDetails(appointmentId);


            if (report == null)
            {
                return NotFound("No report found for this appointment.");
            }

            // Ensure related entities are loaded properly
            if (report.Appointment == null || report.Appointment.Doctor == null || report.Appointment.Doctor.User == null)
            {
                return NotFound("Incomplete appointment details. Please contact support.");
            }

            var viewModel = new PatientMedicalReportViewModel
            {
                ReportId = report.ReportId,
                AppointmentId = report.AppointmentId,
                DoctorName = report.Appointment?.Doctor?.User?.Name ?? "N/A",
                AppointmentDate = report.Appointment?.Date.ToDateTime(TimeOnly.MinValue) ?? DateTime.MinValue,
                Issue = report.Issue ?? "N/A",
                Symptoms = report.Symptoms ?? "N/A",
                Diagnosis = report.Diagnosis ?? "N/A",
                Prescription = report.Prescription ?? "N/A"
            };

            return View(viewModel);
        }


    }
}

