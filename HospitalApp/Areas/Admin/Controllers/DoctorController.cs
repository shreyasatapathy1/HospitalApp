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

            var model = new CreateDoctorViewModel
            {
                Email = doctor.User.Email,
                Name = doctor.User.Name,
                PhoneNumber = doctor.User.PhoneNumber,
                Specialization = doctor.Specialty,
                Qualification = doctor.Qualifications,
                ExperienceInYears = doctor.ExperienceInYears
            };

            return View(model);
        }

        // POST: Edit Doctor
        [HttpPost]
        public async Task<IActionResult> EditDoctor(int id, CreateDoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doctor = _db.Doctors.Include(d => d.User).FirstOrDefault(d => d.Id == id);
                if (doctor == null)
                {
                    return NotFound();
                }

                doctor.User.Email = model.Email;
                doctor.User.Name = model.Name;
                doctor.User.PhoneNumber = model.PhoneNumber;
                doctor.Specialty = model.Specialization;
                doctor.Qualifications = model.Qualification;
                doctor.ExperienceInYears = model.ExperienceInYears;

                _db.Doctors.Update(doctor);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(ViewDoctors));
            }

            return View(model);
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
