using btre2.Data;
using btre2.Models;
using btre2.Repository.Interface;
using btre2.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace btre2.Repository.Manager
{
    public class RealtorRepository : IRealtorRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public RealtorRepository(
            ApplicationDbContext context,
            IWebHostEnvironment hostEnvironment
            )
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
        }

        public string CreateRealtor(CreateRealtorViewModel model)
        {
            string uniqueFileName = UploadedFile(model.Image);
            Realtor realtor = new Realtor
            {
                Name = model.Name,
                Description = model.Description,
                Phone = model.Phone,
                Email = model.Email,
                Image = uniqueFileName,
                IsMvp = model.IsMvp
            };

            context.Realtors.Add(realtor);
            context.SaveChanges();
            return "Successfully Created";
        }

        private String UploadedFile(IFormFile photo)
        {
            string uniqueFileName = null;
            if (photo != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images", "realtors");
                uniqueFileName = DateTime.Now.ToString("yyyyMMdd") + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public Realtor GetRealtor(int id)
        {
            var realtor = context.Realtors.FirstOrDefault(x => x.Id == id);
            return realtor;
        }

        public IEnumerable<Realtor> GetRealtors()
        {
            var realtors = context.Realtors.ToList();
            return realtors;
        }

        public Realtor Update(Realtor realtorChanges)
        {
            var realtor = context.Realtors.Attach(realtorChanges);
            realtor.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return realtorChanges;
        }

        public string Delete(int id)
        {
            var realtor = GetRealtor(id);
            context.Realtors.Remove(realtor);
            context.SaveChanges();
            return "Successfully Deleted";
        }
    }
}
