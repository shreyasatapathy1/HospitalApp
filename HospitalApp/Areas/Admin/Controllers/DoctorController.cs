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
            return View("~/Areas/Admin/Views/AdminDashboardView/Index.cshtml");
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
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Name = model.Name
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Doctor");

                    var doctor = new Models.Doctor
                    {
                        UserId = user.Id,
                        Specialty = model.Specialization,
                        Qualifications = model.Qualification,
                        ExperienceInYears = model.ExperienceInYears
                    };
                    _db.Doctors.Add(doctor);
                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(ViewDoctors));
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


       
        // GET: Edit Doctor
        public IActionResult EditDoctor(int id)
        {
            var doctor = _db.Doctors.Include(d => d.User).FirstOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Edit Doctor
        [HttpPost]
        public async Task<IActionResult> EditDoctor(Models.Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var existingDoctor = _db.Doctors.Include(d => d.User).FirstOrDefault(d => d.Id == doctor.Id);
                if (existingDoctor == null)
                {
                    return NotFound();
                }

                // Update User fields
                existingDoctor.User.Email = doctor.User.Email;
                existingDoctor.User.Name = doctor.User.Name;
                existingDoctor.User.PhoneNumber = doctor.User.PhoneNumber;

                // Update Doctor fields
                existingDoctor.Specialty = doctor.Specialty;
                existingDoctor.Qualifications = doctor.Qualifications;
                existingDoctor.ExperienceInYears = doctor.ExperienceInYears;

                _db.Doctors.Update(existingDoctor);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(ViewDoctors));
            }
            return View(doctor);
        }



        // GET: Delete Doctor
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _db.Doctors.Include(d => d.User).FirstOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost, ActionName("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctorConfirmed(int id)
        {
            var doctor = _db.Doctors.Include(d => d.User).FirstOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            _db.Doctors.Remove(doctor);
            _db.Users.Remove(doctor.User); // Remove associated identity user
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(ViewDoctors));
        }
    }
}
