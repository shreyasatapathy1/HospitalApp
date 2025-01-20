using HospitalApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    public DateOnly Date { get; set; }

    [Required]
    public string TimeSlot { get; set; }  // New property for booking slot

    [Required, MaxLength(50)]
    public string Status { get; set; } // e.g., Pending, Accepted, Rejected

    [MaxLength(1000)]
    public string Notes { get; set; }

    public bool IsPaid { get; set; }  // Payment status

    [MaxLength(50)]
    public string PaymentMethod { get; set; }  // e.g., Credit Card, PayPal
}
