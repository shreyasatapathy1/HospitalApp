using HospitalApp.Data;
using HospitalApp.Data.Repository;
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
        private readonly IDoctorRepository _doctorRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public DoctorController(IDoctorRepository doctorRepository, UserManager<IdentityUser> userManager)
        {
            _doctorRepository = doctorRepository;
            _userManager = userManager;
        }

        public IActionResult ViewDoctors()
        {
            var doctors = _doctorRepository.GetAll();
            return View(doctors);
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
                    _doctorRepository.Add(doctor);

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

        public IActionResult EditDoctor(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> EditDoctor(Models.Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                var existingDoctor = _doctorRepository.GetById(doctor.Id);
                if (existingDoctor == null)
                {
                    return NotFound();
                }
                // Update User fields
                var user = await _userManager.FindByIdAsync(existingDoctor.UserId) as ApplicationUser;
                user.Email = doctor.User.Email;
                user.Name = doctor.User.Name;

                user.PhoneNumber = doctor.User.PhoneNumber;
                await _userManager.UpdateAsync(user);

                // Update Doctor fields
                existingDoctor.Specialty = doctor.Specialty;
                existingDoctor.Qualifications = doctor.Qualifications;
                existingDoctor.ExperienceInYears = doctor.ExperienceInYears;

                _doctorRepository.Update(existingDoctor);

                return RedirectToAction(nameof(ViewDoctors));
            }
            return View(doctor);
        }
        
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost, ActionName("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctorConfirmed(int id)
        {
            var doctor = _doctorRepository.GetById(id);
            if (doctor == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(doctor.UserId);
            _doctorRepository.Delete(doctor);
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(ViewDoctors));
        }
    }
}
