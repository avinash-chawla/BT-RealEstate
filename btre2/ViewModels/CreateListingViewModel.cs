using btre2.Models;
using btre2.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace btre2.ViewModels
{
    public class CreateListingViewModel
    {
        private readonly IListingRepository _listingRepository;
        private readonly IRealtorRepository _realtorRepository;

        public CreateListingViewModel()
        {
            this.Garage = 0;
            this.IsPublished = true;
        }

        public CreateListingViewModel(IListingRepository listingRepository, IRealtorRepository realtorRepository)
        {
            this.Garage = 0;
            this.IsPublished = true;
            _listingRepository = listingRepository;
            _realtorRepository = realtorRepository;
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Bedrooms { get; set; }

        [Required]
        public int Bathrooms { get; set; }
        public int Garage { get; set; }

        [Required]
        public int Sqft { get; set; }
        public decimal LotSize { get; set; }

        [Required]
        public IFormFile PhotoMain { get; set; }
        public IFormFile Photo1 { get; set; }
        public IFormFile Photo2 { get; set; }
        public IFormFile Photo3 { get; set; }
        public IFormFile Photo4 { get; set; }
        public IFormFile Photo5 { get; set; }
        public IFormFile Photo6 { get; set; }
        public Boolean IsPublished { get; set; }
        public int RealtorId { get; set; }
        public IEnumerable<Realtor> Realtors { get; set; }

    }
}
