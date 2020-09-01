using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using btre2.Extensions;
using btre2.Models;
using btre2.Repository.Interface;
using btre2.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace btre2.Controllers
{
    public class ListingController : Controller
    {
        private readonly IListingRepository listingRepo;
        private readonly IRealtorRepository realtorRepo;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ListingController(
            IListingRepository listingRepo,
            IRealtorRepository realtorRepo,
            IWebHostEnvironment hostEnvironment,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.listingRepo = listingRepo;
            this.realtorRepo = realtorRepo;
            this.hostEnvironment = hostEnvironment;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Listings()
        {
            if(User.IsInRole("Admin"))
            {
                var list = listingRepo.GetListings();
                return View(list);
            }
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var listings = listingRepo.GetListingsForSpecificRealtor(userEmail);
            return View(listings);
        }

        public IActionResult CreateListing()
        {
            CreateListingViewModel model = new CreateListingViewModel
            {
                Realtors = realtorRepo.GetRealtors()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateListing(CreateListingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = listingRepo.CreateListing(model);
                return RedirectToAction(nameof(Listings));
            }
            return View();
        }

        public IActionResult EditListing(int id)
        {
            var listing = listingRepo.GetListing(id);
            EditListingViewModel model = new EditListingViewModel()
            {
                Id = listing.Id,
                Title = listing.Title,
                Address = listing.Address,
                City = listing.City,
                State = listing.State,
                ZipCode = listing.ZipCode,
                Description = listing.Description,
                Price = listing.Price,
                Bedrooms = listing.Bedrooms,
                Bathrooms = listing.Bathrooms,
                Garage = listing.Garage,
                Sqft = listing.Sqft,
                LotSize = listing.LotSize,
                IsPublished = listing.IsPublished,
                RealtorId = listing.RealtorId,
                Realtors = realtorRepo.GetRealtors(),
                ExistingPhotoMain = listing.PhotoMain,
                ExistingPhoto1 = listing.Photo1,
                ExistingPhoto2 = listing.Photo2,
                ExistingPhoto3 = listing.Photo3,
                ExistingPhoto4 = listing.Photo4,
                ExistingPhoto5 = listing.Photo5,
                ExistingPhoto6 = listing.Photo6,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditListing(EditListingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var listing = listingRepo.GetListing(model.Id);
                listing.Title = model.Title;
                listing.Title = model.Title;
                listing.Address = model.Address;
                listing.City = model.City;
                listing.State = model.State;
                listing.ZipCode = model.ZipCode;
                listing.Description = model.Description;
                listing.Price = model.Price;
                listing.Bedrooms = model.Bedrooms;
                listing.Bathrooms = model.Bathrooms;
                listing.Garage = model.Garage;
                listing.Sqft = model.Sqft;
                listing.LotSize = model.LotSize;
                listing.IsPublished = model.IsPublished;
                listing.RealtorId = model.RealtorId;

                if (model.PhotoMain != null)
                {
                    if (model.ExistingPhotoMain != null)
                    {
                        string filePath = Path.Combine(hostEnvironment.WebRootPath, "images", model.ExistingPhotoMain);
                        System.IO.File.Delete(filePath);
                    }
                    listing.PhotoMain = UploadedFile(model.PhotoMain);
                };
                if (model.Photo1 != null)
                {
                    if (model.ExistingPhoto1 != null)
                    {
                        string filePath = Path.Combine(hostEnvironment.WebRootPath, "images", model.ExistingPhoto1);
                        System.IO.File.Delete(filePath);
                    }
                    listing.Photo1 = UploadedFile(model.Photo1);
                };
                if (model.Photo2 != null)
                {
                    if (model.ExistingPhoto2 != null)
                    {
                        string filePath = Path.Combine(hostEnvironment.WebRootPath, "images", model.ExistingPhoto2);
                        System.IO.File.Delete(filePath);
                    }
                    listing.Photo2 = UploadedFile(model.Photo2);
                };
                if (model.Photo3 != null)
                {
                    if (model.ExistingPhoto3 != null)
                    {
                        string filePath = Path.Combine(hostEnvironment.WebRootPath, "images", model.ExistingPhoto3);
                        System.IO.File.Delete(filePath);
                    }
                    listing.Photo3 = UploadedFile(model.Photo3);
                };
                if (model.Photo4 != null)
                {
                    if (model.ExistingPhoto4 != null)
                    {
                        string filePath = Path.Combine(hostEnvironment.WebRootPath, "images", model.ExistingPhoto4);
                        System.IO.File.Delete(filePath);
                    }
                    listing.Photo4 = UploadedFile(model.Photo4);
                };
                if (model.Photo5 != null)
                {
                    if (model.ExistingPhoto5 != null)
                    {
                        string filePath = Path.Combine(hostEnvironment.WebRootPath, "images", model.ExistingPhoto5);
                        System.IO.File.Delete(filePath);
                    }
                    listing.Photo5 = UploadedFile(model.Photo5);
                };
                if (model.Photo6 != null)
                {
                    if (model.ExistingPhoto6 != null)
                    {
                        string filePath = Path.Combine(hostEnvironment.WebRootPath, "images", model.ExistingPhoto6);
                        System.IO.File.Delete(filePath);
                    }
                    listing.Photo6 = UploadedFile(model.Photo6);
                };

                Listing updatedListing = listingRepo.Update(listing);
                return RedirectToAction(nameof(Listings));
            }
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
        public IActionResult DeleteListing(int id)
        {
            var result = listingRepo.Delete(id);
            return RedirectToAction(nameof(Listings)); ;
        }
    }
}
