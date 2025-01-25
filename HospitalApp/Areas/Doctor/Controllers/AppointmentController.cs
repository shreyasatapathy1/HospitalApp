using HospitalApp.Data.Repository;
using HospitalApp.Models;
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
        private readonly IMedicalReportRepository _medicalReportRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository, UserManager<IdentityUser> userManager, IDoctorRepository doctorRepository, IMedicalReportRepository medicalReportRepository)
        {
            _appointmentRepository = appointmentRepository;
            _userManager = userManager;
            _doctorRepository = doctorRepository;
            _medicalReportRepository = medicalReportRepository;
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

        public IActionResult MedicalReport(int appointmentId)
        {
            var appointment = _appointmentRepository.GetAppointmentWithDetails(appointmentId);
            if (appointment == null || appointment.Patient == null || appointment.Patient.User == null)
            {
                return NotFound("Appointment or associated patient details not found.");
            }

            var existingReport = _medicalReportRepository.Find(r => r.AppointmentId == appointmentId).FirstOrDefault();

            var reportViewModel = new MedicalReportViewModel
            {
                AppointmentId = appointment.Id,
                PatientName = appointment.Patient.User.Name,
                Email = appointment.Patient.User.Email,
                PhoneNumber = appointment.Patient.User.PhoneNumber,
                Age = appointment.Patient.Age,
                Weight = appointment.Patient.Weight,
                IsReportExisting = existingReport != null
            };

            if (existingReport != null)
            {
                reportViewModel.ReportId = existingReport.ReportId;
                reportViewModel.Issue = existingReport.Issue;
                reportViewModel.Symptoms = existingReport.Symptoms;
                reportViewModel.Diagnosis = existingReport.Diagnosis;
                reportViewModel.Prescription = existingReport.Prescription;
            }

            return View(reportViewModel);
        }


        [HttpPost]
        public IActionResult MedicalReport(MedicalReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ReportId == 0)
                {
                    // Save new report
                    var report = new GenerateReport
                    {
                        AppointmentId = model.AppointmentId,
                        Issue = model.Issue,
                        Symptoms = model.Symptoms,
                        Diagnosis = model.Diagnosis,
                        Prescription = model.Prescription,
                        Date = DateTime.Now
                    };

                    _medicalReportRepository.Add(report);
                }
                else
                {
                    // Update existing report
                    var existingReport = _medicalReportRepository.GetById(model.ReportId);
                    if (existingReport != null)
                    {
                        existingReport.Issue = model.Issue;
                        existingReport.Symptoms = model.Symptoms;
                        existingReport.Diagnosis = model.Diagnosis;
                        existingReport.Prescription = model.Prescription;

                        _medicalReportRepository.Update(existingReport);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}
