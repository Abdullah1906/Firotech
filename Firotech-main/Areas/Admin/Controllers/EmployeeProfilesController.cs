using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firotechbd.Areas.Admin.Models;
using Firotechbd.Data;
using Firotechbd.Areas.Admin.ViewModels;
using Firotechbd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace Firotechbd.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Employee/Profile")]
    [Authorize(Roles = "Admin")]
    public class EmployeeProfilesController : Controller
    {
        private readonly FirotechbdContext _context;
        private readonly FileUploadHelper _fileUploadHelper;
        public EmployeeProfilesController(FirotechbdContext context, FileUploadHelper fileUploadHelper)
        {
            _context = context;
            _fileUploadHelper = fileUploadHelper;

        }

    // GET: Admin/EmployeeProfiles
        [HttpGet]
        [Route("Active")]
        public async Task<IActionResult>Active()
        {
            return _context.EmployeePubProfile != null ?
                        View(await _context.EmployeePubProfile.ToListAsync()) :
                        Problem("Entity set 'FirotechbdContext.EmployeePubProfile'  is null.");
        }
        [HttpGet]
        [Route("Inactive")]
        public async Task<IActionResult> Inactive()
        {
            return _context.EmployeePubProfile != null ?
                        View(await _context.EmployeePubProfile.ToListAsync()) :
                        Problem("Entity set 'FirotechbdContext.EmployeePubProfile'  is null.");
        }

        // GET: Admin/EmployeeProfiles/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create(EmployeePubProfileVM model)
        
        {
            model.PositionDropdown = GetPosition();

            // we can pass data by getstatus for that we have to change in view page(create)
            //model.StatusDropdown = GetStatus();
            ViewBag.StatusList = _context.Lookup.Where(x => x.Type == LookupType.Status && x.Id > 0)
                    .OrderBy(x => x.Serial).ToList();

            return View(model);
        }        

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var employeeProfile = await _context.EmployeePubProfile.FindAsync(id);
            if (employeeProfile == null)
            {
                return NotFound();
            }
            var employeePubProfileVM = new EmployeePubProfileVM
            {
                Id = employeeProfile.Id,
                Slug = employeeProfile.Slug,
                EmployeeProfileImageUrl = employeeProfile.EmployeeProfileImageUrl,
                EmployeeName = employeeProfile.EmployeeName,
                EmployeePhone = employeeProfile.EmployeePhone,
                EmployeeEmail = employeeProfile.EmployeeEmail,
                EmployeeAddress = employeeProfile.EmployeeAddress,
                EmployeePosition = employeeProfile.EmployeePosition,
                EmployeeStatus = employeeProfile.EmployeeStatus,
                EmployeeAboutme = employeeProfile.EmployeeAboutme,
                Employeefb = employeeProfile.Employeefb,
                Employeewp = employeeProfile.Employeewp,
                Employeex = employeeProfile.Employeex,
                EmployeeMailHot = employeeProfile.EmployeeMailHot,
                EmployeeLinkedin = employeeProfile.EmployeeLinkedin,
                IsActive = employeeProfile.IsActive
            };

            employeePubProfileVM.PositionDropdown = GetPosition();

            // we can pass data by getstatus (this way)
            //employeePubProfileVM.PositionDropdown = GetStatus();

            ViewBag.StatusList = _context.Lookup.Where(x => x.Type == LookupType.Status && x.Id > 0)
                .OrderBy(x => x.Serial).ToList();
            return View("Create", employeePubProfileVM);
        }

        // pass data by model
        // method for position from database
        public List<Dropdown> GetPosition()
        {
            var PositionList = _context.Lookup.Where(x => x.Type == LookupType.Position && x.Id > 0)
                .OrderBy(x => x.Serial).ToList();
            return PositionList.Select(x => new Dropdown

            {
                Id = x.Id,
                Value = x.Value,
                Name = x.Name,
                Type = x.Type,
                Serial = x.Serial,
                IsActive = x.IsActive
            }).ToList();
        }

        // pass data by model
        // method for Status from database
        public List<Dropdown> GetStatus()
        {
            var PositionList = _context.Lookup.Where(x => x.Type == LookupType.Status && x.Id > 0)
                .OrderBy(x => x.Serial).ToList();
            return PositionList.Select(x => new Dropdown

            {
                Id = x.Id,
                Value = x.Value,
                Name = x.Name,
                Type = x.Type,
                Serial = x.Serial,
                IsActive = x.IsActive
            }).ToList();
        }



        [HttpPost]
        [Route("EmpProfile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeProfile(EmployeePubProfileVM employeePubProfile, List<IFormFile> EmployeeProfileImage)
        {
            if (employeePubProfile != null)
            {
                if (employeePubProfile.Id == 0)
                {
                    return await CreateNew(employeePubProfile, EmployeeProfileImage);
                }
                else
                {
                    // Call update function if the profile already exists
                    await UpdateProfile(employeePubProfile, EmployeeProfileImage);
                }
            }
            else
            {
                ModelState.AddModelError("", "Employee profile data is required.");
                return View(employeePubProfile);
            }

            return Json(new { success = true, message = "Saved" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNew(EmployeePubProfileVM employeePubProfile, List<IFormFile> EmployeeProfileImage)
        {

            if (employeePubProfile == null || employeePubProfile.Id != 0)
            {
                ModelState.AddModelError("", "Invalid employee profile data.");
                return View(employeePubProfile);
            }


            // Upload the profile image
            var (success, fileName, message) = await _fileUploadHelper.UploadFileAsync(EmployeeProfileImage, UploadFilePath.EmployeeProfiles);
            if (success)
            {
                employeePubProfile.EmployeeProfileImageUrl = fileName;
            }

            // Map the view model to the entity model
            var employeeProfile = new EmployeePubProfile
            {
                Slug = employeePubProfile.Slug,
                EmployeeProfileImageUrl = employeePubProfile.EmployeeProfileImageUrl,
                EmployeeName = employeePubProfile.EmployeeName,
                EmployeePhone = employeePubProfile.EmployeePhone,
                EmployeeEmail = employeePubProfile.EmployeeEmail,
                EmployeeAddress = employeePubProfile.EmployeeAddress,
                EmployeePosition = employeePubProfile.EmployeePosition,
                EmployeeStatus = employeePubProfile.EmployeeStatus,
                EmployeeAboutme = employeePubProfile.EmployeeAboutme,
                Employeefb = employeePubProfile.Employeefb,
                Employeewp = employeePubProfile.Employeewp,
                Employeex = employeePubProfile.Employeex,
                EmployeeMailHot = employeePubProfile.EmployeeMailHot,
                EmployeeLinkedin = employeePubProfile.EmployeeLinkedin,
                IsActive = employeePubProfile.IsActive,
                CreateAt = DateTime.Now,
                CreateBy = Guid.NewGuid(),
                UpdateAt = DateTime.Now,
                UpdateBy = Guid.NewGuid()
            };

            // Save the new employee profile to the database
            await _context.AddAsync(employeeProfile);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Saved" });
        }
        [HttpPost]
        [Route("UpdateProfile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(EmployeePubProfileVM employeePubProfile, List<IFormFile> EmployeeProfileImage)
        {
            // Ensure that the model is valid and the ID is not zero
            if (employeePubProfile == null || employeePubProfile.Id == 0)
            {
                ModelState.AddModelError("", "Invalid employee profile data.");
                return View(employeePubProfile);
            }

            // Get the existing profile from the database
            var existingProfile = await _context.EmployeePubProfile.FindAsync(employeePubProfile.Id);
            if (existingProfile == null)
            {
                ModelState.AddModelError("", "Employee profile not found.");
                return View(employeePubProfile);
            }

            // Update the profile properties
            existingProfile.Slug = employeePubProfile.Slug;
            existingProfile.EmployeeName = employeePubProfile.EmployeeName;
            existingProfile.EmployeePhone = employeePubProfile.EmployeePhone;
            existingProfile.EmployeeEmail = employeePubProfile.EmployeeEmail;
            existingProfile.EmployeeAddress = employeePubProfile.EmployeeAddress;
            existingProfile.EmployeePosition = employeePubProfile.EmployeePosition;
            existingProfile.EmployeeStatus = employeePubProfile.EmployeeStatus;
            existingProfile.EmployeeAboutme = employeePubProfile.EmployeeAboutme;
            existingProfile.Employeefb = employeePubProfile.Employeefb;
            existingProfile.Employeewp = employeePubProfile.Employeewp;
            existingProfile.Employeex = employeePubProfile.Employeex;
            existingProfile.EmployeeMailHot = employeePubProfile.EmployeeMailHot;
            existingProfile.EmployeeLinkedin = employeePubProfile.EmployeeLinkedin;
            existingProfile.IsActive = employeePubProfile.IsActive;
            existingProfile.UpdateAt = DateTime.Now;
            existingProfile.UpdateBy = Guid.NewGuid();

            // If a new profile image is uploaded, handle the upload
            if (EmployeeProfileImage != null && EmployeeProfileImage.Count > 0)
            {
                var (success, fileName, message) = await _fileUploadHelper.UploadFileAsync(EmployeeProfileImage, UploadFilePath.EmployeeProfiles);
                if (success)
                {
                    existingProfile.EmployeeProfileImageUrl = fileName; // Update the image URL
                }
                else
                {
                    ModelState.AddModelError("", message); // Add error message if file upload failed
                    return View(employeePubProfile);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Active));
        }




        private bool EmployeePubProfileExists(int id)
        {
            return (_context.EmployeePubProfile?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
