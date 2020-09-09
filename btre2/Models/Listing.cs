using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace btre2.Models
{
    public class Listing
    {
        public Listing()
        {
            this.Garage = 0;
            this.IsPublished = true;
            this.ListDate = DateTime.Now;
        }

        [Key]
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
        public string PhotoMain { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Photo4 { get; set; }
        public string Photo5 { get; set; }
        public string Photo6 { get; set; }
        public Boolean IsPublished { get; set; }
        public DateTime? ListDate { get; set; }
        public int RealtorId { get; set; }

        [ForeignKey("RealtorId")]
        public Realtor Realtor { get; set; }
    }
}
