using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models.ViewModel
{
    public class MedicalReportViewModel
    {
        public int ReportId { get; set; }

        [Required]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Weight")]
        public float Weight { get; set; }

        [Required]
        [Display(Name = "Issue")]
        public string Issue { get; set; }

        [Required]
        [Display(Name = "Symptoms")]
        public string Symptoms { get; set; }

        [Required]
        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }

        [Required]
        [Display(Name = "Prescription")]
        public string Prescription { get; set; }

        public int AppointmentId { get; set; }

        public bool IsReportExisting { get; set; } // To check if report exists for editing
    }
}
