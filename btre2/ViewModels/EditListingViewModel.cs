using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace btre2.ViewModels
{
    public class EditListingViewModel : CreateListingViewModel
    {
        public new IFormFile PhotoMain { get; set; }
        public string ExistingPhotoMain { get; set; }
        public string ExistingPhoto1 { get; set; }
        public string ExistingPhoto2 { get; set; }
        public string ExistingPhoto3 { get; set; }
        public string ExistingPhoto4 { get; set; }
        public string ExistingPhoto5 { get; set; }
        public string ExistingPhoto6 { get; set; }
    }
}
