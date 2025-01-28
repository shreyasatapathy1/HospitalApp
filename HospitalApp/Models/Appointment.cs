using HospitalApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HospitalApp.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        [JsonIgnore]
        [ValidateNever]
        public Doctor Doctor { get; set; }//navigation

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        [JsonIgnore]
        [ValidateNever]
        public Patient Patient { get; set; }//navigation

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public string TimeSlot { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; } // e.g., Pending, Confirmed, Completed

        [MaxLength(1000)]
        public string Notes { get; set; }

        public bool IsPaid { get; set; }  // Payment status

        [MaxLength(50)]
        public string PaymentMethod { get; set; }  // e.g., Cash
    }
}
