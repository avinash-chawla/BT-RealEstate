using btre2.Models;
using btre2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace btre2.Repository.Interface
{
    public interface IListingRepository
    {
        IEnumerable<Listing> GetListings();
        Listing GetListing(int id);
        string CreateListing(CreateListingViewModel model);
        IEnumerable<Listing> GetTop3Listing();
        Listing Update(Listing listingChanges);
        string Delete(int id);
        IEnumerable<Listing> Search(SearchViewModel model);
        IEnumerable<Listing> GetPagedListings(int pageIndex);
        IEnumerable<Listing> GetListingsForSpecificRealtor(string loggedInUserEmail);
    }
}
