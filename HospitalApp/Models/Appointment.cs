using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; } // e.g., Pending, Accepted, Rejected

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
