using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required, Range(0, 120)]
        public int Age { get; set; }

        [Required]
        public float Weight { get; set; }

        [MaxLength(1000)]
        public string History { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        public virtual List<Appointment> Appointments { get; set; }
    }
}
