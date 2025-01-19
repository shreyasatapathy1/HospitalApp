using HospitalApp.Data;
using HospitalApp.Models;
using HospitalApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public DoctorController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewDoctors()
        {
            var doctorList = _db.Doctors.Include(d => d.User).ToList();
            return View(doctorList);
        }



        public IActionResult AddDoctor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDoctor(CreateDoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create User in Identity
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Name = model.Name // Assigning the Name property
                    
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Add user to Doctor role
                    await _userManager.AddToRoleAsync(user, "Doctor");

                    // Add details to Doctors table
                    var doctor = new Models.Doctor
                    {
                        UserId = user.Id,
                        Specialty = model.Specialization,
                        Qualifications = model.Qualification,
                        ExperienceInYears = model.ExperienceInYears
                    };
                    _db.Doctors.Add(doctor);
                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index)); // Redirect to a list of doctors or another page
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        









    }
}
