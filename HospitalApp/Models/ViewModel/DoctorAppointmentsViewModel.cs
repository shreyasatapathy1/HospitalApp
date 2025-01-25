using System.ComponentModel.DataAnnotations;
using HospitalApp.Models;
using System.Collections.Generic;

namespace HospitalApp.Models.ViewModel
{
    public class DoctorAppointmentsViewModel
    {
        [Required]
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }

        [Required]
        [Display(Name = "Upcoming Appointments")]
        public List<Appointment> UpcomingAppointments { get; set; }
    }
}
