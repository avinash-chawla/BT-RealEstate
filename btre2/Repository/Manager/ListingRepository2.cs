using btre2.Data;
using btre2.Models;
using btre2.Repository.Interface;
using btre2.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;


namespace btre2.Repository.Manager
{
    public class ListingRepository2 : IListingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _connectionString;

        public ListingRepository2(
                            ApplicationDbContext context,
                            IWebHostEnvironment hostEnvironment,
                            UserManager<ApplicationUser> userManager,
                            IHttpContextAccessor httpContextAccessor,
                            IConfiguration configuration)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public Listing GetListing(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetListingById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ListingId", Id));
                    Listing listing = null;
                    sql.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listing = MapToValue(reader);
                        }
                    }
                    return listing;
                }
            }
        }

        public IEnumerable<Listing> GetListings()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllListings", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var response = new List<Listing>();
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }
                    return response;
                }
            }
        }

        private Listing MapToValue(SqlDataReader reader)
        {
            var listing = new Listing()
            {
                Id = (int)reader["Id"],
                Title = reader["Title"].ToString(),
                Address = reader["Address"].ToString(),
                City = reader["City"].ToString(),
                State = reader["State"].ToString(),
                ZipCode = reader["ZipCode"].ToString(),
                Description = reader["Description"].ToString(),
                Price = (decimal)reader["Price"],
                Bedrooms = (int)reader["Bedrooms"],
                Bathrooms = (int)reader["Bathrooms"],
                Garage = (int)reader["Garage"],
                Sqft = (int)reader["Sqft"],
                LotSize = (decimal)reader["LotSize"],
                PhotoMain = reader["PhotoMain"].ToString(),
                Photo1 = reader["Photo1"].ToString(),
                Photo2 = reader["Photo2"].ToString(),
                Photo3 = reader["Photo3"].ToString(),
                Photo4 = reader["Photo4"].ToString(),
                Photo5 = reader["Photo5"].ToString(),
                Photo6 = reader["Photo6"].ToString(),
                IsPublished = (bool)reader["IsPublished"],
                ListDate = (DateTime)reader["ListDate"],
                RealtorId = (int) reader["RealtorId"]
            };

            //var realtor = new Realtor()
            //{
            //    Name = reader["R_Name"].ToString(),
            //    Phone = reader["R_Phone"].ToString(),
            //    Email = reader["R_Email"].ToString(),
            //    Description = reader["R_Description"].ToString(),
            //    IsMvp = (bool) reader["R_IsMvp"],
            //    Image = reader["R_Image"].ToString()
            //};

            //listing.Realtor = realtor;
            return listing;
        }

        public IEnumerable<Listing> GetTop3Listing()
        {
            var listings = _context.Listings
                                        .Include(x => x.Realtor)
                                        .OrderByDescending(x => x.ListDate)
                                        .Take(3)
                                        .ToList();
            return listings;
        }

        public string CreateListing(CreateListingViewModel model)
        {
            var uniqueFileName = UploadedFile(model.PhotoMain);
            var uniqueFileName1 = UploadedFile(model.Photo1);
            var uniqueFileName2 = UploadedFile(model.Photo2);
            var uniqueFileName3 = UploadedFile(model.Photo3);
            var uniqueFileName4 = UploadedFile(model.Photo4);
            var uniqueFileName5 = UploadedFile(model.Photo5);
            var uniqueFileName6 = UploadedFile(model.Photo6);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddListing", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@Address", model.Address);
                cmd.Parameters.AddWithValue("@City", model.City);
                cmd.Parameters.AddWithValue("@State", model.State);
                cmd.Parameters.AddWithValue("@ZipCode", model.ZipCode);
                cmd.Parameters.AddWithValue("@Description", model.Description);
                cmd.Parameters.AddWithValue("@Price", model.Price);
                cmd.Parameters.AddWithValue("@Bedrooms", model.Bedrooms);
                cmd.Parameters.AddWithValue("@Bathrooms", model.Bathrooms);
                cmd.Parameters.AddWithValue("@Garage", model.Garage);
                cmd.Parameters.AddWithValue("@Sqft", model.Sqft);
                cmd.Parameters.AddWithValue("@LotSize", model.LotSize);
                cmd.Parameters.AddWithValue("@PhotoMain", uniqueFileName);
                cmd.Parameters.AddWithValue("@Photo1", uniqueFileName1);
                cmd.Parameters.AddWithValue("@Photo2", uniqueFileName2);
                cmd.Parameters.AddWithValue("@Photo3", uniqueFileName3);
                cmd.Parameters.AddWithValue("@Photo4", uniqueFileName4);
                cmd.Parameters.AddWithValue("@Photo5", uniqueFileName5);
                cmd.Parameters.AddWithValue("@Photo6", uniqueFileName6);
                cmd.Parameters.AddWithValue("@IsPublished", model.IsPublished);
                cmd.Parameters.AddWithValue("@RealtorId", model.RealtorId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return "Successfully Created";
        }

        private String UploadedFile(IFormFile photo)
        {
            string uniqueFileName = null;
            if (photo != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                uniqueFileName = DateTime.Now.ToString("yyyyMMdd") + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public Listing Update(Listing listingChanges)
        {
            var listing = _context.Listings.Attach(listingChanges);
            listing.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return listingChanges;
        }

        public string Delete(int id)
        {
            var listing = _context.Listings.Find(id);
            _context.Listings.Remove(listing);
            _context.SaveChanges();
            return "Successfully Deleted";
        }

        public IEnumerable<Listing> Search(SearchViewModel model)
        {
            IEnumerable<Listing> listings = _context.Listings.Include(x => x.Realtor);
            if (model.Bedrooms.HasValue)
            {
                listings = listings.Where(m => m.Bedrooms <= model.Bedrooms).OrderByDescending(x => x.Bedrooms);
            }

            if (model.Price.HasValue)
            {
                listings = listings.Where(m => m.Price <= model.Price);
            }

            if (!String.IsNullOrEmpty(model.City))
            {
                listings = listings.Where(m => m.City.ToLower().Contains(model.City.ToLower()));
            }

            if (!String.IsNullOrEmpty(model.State))
            {
                listings = listings.Where(m => m.State == model.State);
            }

            if (!String.IsNullOrEmpty(model.Keyword))
            {
                listings = listings.Where(m => m.Description.ToLower().Contains(model.Keyword.ToLower()));
            }

            return listings.ToList();
        }

        public IEnumerable<Listing> GetListingsForSpecificRealtor(string loggedInUserEmail)
        {
            return _context.Listings
                                .Include(x => x.Realtor)
                                .Where(x => x.Realtor.Email == loggedInUserEmail)
                                .ToList();
        }

        public IEnumerable<Listing> GetPagedListings(int pageIndex = 1)
        {
            var pageSize = 3;
            var listings = GetListings();
            var model = PagingList.Create(listings, pageSize, pageIndex);
            model.Action = "Listings";
            return model;
        }
    }
}

