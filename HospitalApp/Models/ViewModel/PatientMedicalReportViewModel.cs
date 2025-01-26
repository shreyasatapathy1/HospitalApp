using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models.ViewModel
{
    public class PatientMedicalReportViewModel
    {
        public int ReportId { get; set; }

        [Display(Name = "Appointment ID")]
        public int AppointmentId { get; set; }

        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Issue")]
        public string Issue { get; set; }

        [Display(Name = "Symptoms")]
        public string Symptoms { get; set; }

        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }

        [Display(Name = "Prescription")]
        public string Prescription { get; set; }
    }
}
