using btre2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace btre2.ViewModels
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            this.Listings = new List<Listing>();
        }
        public IEnumerable<Listing> Listings { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Keyword { get; set; }

        [BindProperty(SupportsGet = true)]
        public string City { get; set; }

        [BindProperty(SupportsGet = true)]
        public string State { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Bedrooms { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Price { get; set; }
    }
}
