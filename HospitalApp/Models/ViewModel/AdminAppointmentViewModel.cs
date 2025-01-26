using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models.ViewModel
{
    public class AdminAppointmentViewModel
    {
        [Required]
        [Display(Name = "Appointment ID")]
        public int AppointmentId { get; set; }

        [Required]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        [Required]
        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Time Slot")]
        public string TimeSlot { get; set; }

        [Required]
        [Display(Name = "Appointment Status")]
        public string Status { get; set; }

        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Medical Issue")]
        public string Issue { get; set; }

        [Display(Name = "Patient Medical History")]
        public string MedicalHistory { get; set; }
    }
}
