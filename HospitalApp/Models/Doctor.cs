﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Identity.Client;

namespace HospitalApp.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        
        [Required, MaxLength(100)]
        public string Specialty { get; set; } // Category (e.g., Cardiologist)

        [Required, Phone]
        public string ContactNumber { get; set; }

        [Required, Range(1, 50)]
        public int ExperienceInYears { get; set; }

        [Required, MaxLength(200)]
        public string Qualifications { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? User { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}
