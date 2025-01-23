using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HospitalApp.Models
{
    public class GenerateReport
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now; // Date of report generation

        [Required, MaxLength(100)]
        public string Issue { get; set; } // Main issue reported by the patient

        [Required, MaxLength(1000)]
        public string Symptoms { get; set; } // Observed symptoms

        [Required, MaxLength(100)]
        public string Diagnosis { get; set; } // Diagnosis provided by the doctor

        [Required, MaxLength(1000)]
        public string Prescription { get; set; } // Prescribed medications or treatments

       
    }
}
