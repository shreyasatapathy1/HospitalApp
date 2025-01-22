using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models.ViewModel
{
    public class CreateDoctorViewModel
    {
        // Identity User Properties
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; } // This maps to ApplicationUser.Name

        // Doctor Table Properties
        [Required]
        [Display(Name = "Specialization")]
        public string Specialization { get; set; }

        [Required]
        [Display(Name = "Qualification")]
        public string Qualification { get; set; }
        [Required, Range(1, 50)]
        [Display(Name = "Experience In Years")]
        public int ExperienceInYears { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        

    }
}
