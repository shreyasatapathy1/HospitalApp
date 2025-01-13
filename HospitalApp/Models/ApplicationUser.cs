﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HospitalApp.Models
{
    public class ApplicationUser:IdentityUser
    {
        
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; } // "Admin", "Doctor", "Patient"

    }
}
