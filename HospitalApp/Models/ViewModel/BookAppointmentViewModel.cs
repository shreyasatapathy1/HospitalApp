using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models.ViewModel
{
    public class BookAppointmentViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Select Specialization")]
        public string Specialization { get; set; }

        [Required]
        [Display(Name = "Select Doctor")]
        public int DoctorId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateTime Date { get; set; } = DateTime.Now;//Ensure your BookAppointmentViewModel uses DateTime for the date input, as HTML date inputs work with DateTime

        [Required]
        [Display(Name = "Time Slot")]
        public string TimeSlot { get; set; }

        public List<string> TimeSlotOptions { get; set; } = new()
        {
            "9:00 AM - 10:00 AM",
            "10:00 AM - 11:00 AM",
            "11:00 AM - 12:00 PM",
            "2:00 PM - 3:00 PM",
            "3:00 PM - 4:00 PM"
        };

        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        public List<string> PaymentOptions { get; set; } = new()
        {
            "Credit Card",
            "PayPal",
            "Cash"
        };

        public string Notes { get; set; }
    }
}
