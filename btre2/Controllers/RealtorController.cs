using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using btre2.Models;
using btre2.Repository.Interface;
using btre2.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace btre2.Controllers
{
    public class RealtorController : Controller
    {
        private readonly IRealtorRepository realtorRepo;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RealtorController(
            IRealtorRepository realtorRepo,
            IWebHostEnvironment hostEnvironment,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.realtorRepo = realtorRepo;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateRealtor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRealtor(CreateRealtorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user, "Realtor@1");

                if (result.Succeeded)
                {
                    if (model.IsMvp)
                    {
                        await userManager.AddToRoleAsync(user, "Realtor");
                    }
                    var message = realtorRepo.CreateRealtor(model);
                    return RedirectToAction(nameof(Realtors));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                };
            }
            return View();
        }

        public IActionResult Realtors()
        {
            var realtors = realtorRepo.GetRealtors();
            return View(realtors);
        }

        

        [HttpGet]
        public IActionResult EditRealtor(int id)
        {
            var realtor = realtorRepo.GetRealtor(id);
            EditRealtorViewModel model = new EditRealtorViewModel
            {
                Name = realtor.Name,
                Description = realtor.Description,
                Email = realtor.Email,
                Phone = realtor.Phone,
                IsMvp = realtor.IsMvp,
                ExistingImage = realtor.Image
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRealtor(EditRealtorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var realtor = realtorRepo.GetRealtor(model.Id);
                realtor.Name = model.Name;
                realtor.Description = model.Description;
                realtor.Email = model.Email;
                realtor.Phone = model.Phone;
                realtor.IsMvp = model.IsMvp;

                var user = await userManager.FindByEmailAsync(model.Email);
                if (model.IsMvp)
                {
                    await userManager.AddToRoleAsync(user, "Realtor");
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, "Realtor");
                }

                if (model.Image != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(hostEnvironment.WebRootPath, "images", "realtors", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    };
                    realtor.Image = UploadedFile(model.Image);
                };
                Realtor updatedRealtor = realtorRepo.Update(realtor);
                return RedirectToAction("Index");
            };
            return View();
        }

        private String UploadedFile(IFormFile photo)
        {
            string uniqueFileName = null;
            if (photo != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                uniqueFileName = DateTime.Now.ToString("yyyyMMdd") + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult DeleteRealtor(int id)
        {
            var result = realtorRepo.Delete(id);
            return RedirectToAction(nameof(Realtors));
        }
    }
}
