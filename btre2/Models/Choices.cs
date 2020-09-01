using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace btre2.Models
{
    public class Choices
    {
        public Dictionary<int, int> Bedrooms { get; set; }
        public Dictionary<decimal, string> Price { get; set; }
        public Dictionary<string, string> State { get; set; }
        public Choices()
        {
            Bedrooms = new Dictionary<int, int>
            {
                {1,1},
                {2,2},
                {3,3},
                {4,4},
                {5,5},
                {6,6},
                {7,7},
                {8,8},
                {9,9},
                {10,10},
            };

            Price = new Dictionary<decimal, string>
            {
                {100000, "100,000" },
                {200000, "200,000" },
                {300000, "300,000" },
                {400000, "400,000" },
                {500000, "500,000" },
                {600000, "600,000" },
                {700000, "700,000" },
                {800000, "800,000" },
                {900000, "900,000" },
                {1000000, "1M" },
            };

            State = new Dictionary<string, string>
            {
                {"AK", "Alaska" },
                {"AL", "Alabama" },
                {"AR", "Arkansas" },
                {"AS", "American Samoa" },
                {"AZ", "Arizona" },
                {"CA", "California" },
                {"CO", "Colorado" },
                {"CT", "Connecticut" },
                {"DC", "District of Columbia" },
                {"DE", "Delaware" },
                {"FL", "Florida" },
                { "GA", "Georgia" },
                {"GU", "Guam"},
                {"HI", "Hawaii"},
                {"IA", "Iowa"},
                {"ID", "Idaho"},
                {"IL", "Illinois"},
                {"IN", "Indiana"},
                {"KS", "Kansas"},
                {"KY", "Kentucky"},
                {"LA", "Louisiana"},
                {"MA", "Massachusetts"},
                {"MD", "Maryland"},
                {"ME", "Maine"},
                {"MI", "Michigan"},
                {"MN", "Minnesota"},
                {"MO", "Missouri"},
                {"MP", "Northern Mariana Islands"},
                {"MS", "Mississippi"},
                {"MT", "Montana"},
                {"NA", "National"},
                {"NC", "North Carolina"},
                {"ND", "North Dakota"},
                {"NE", "Nebraska"},
                {"NH", "New Hampshire"},
                {"NJ", "New Jersey"},
                {"NM", "New Mexico"},
                {"NV", "Nevada"},
                {"NY", "New York"},
                {"OH", "Ohio"},
                {"OK", "Oklahoma"},
                {"OR", "Oregon"},
                {"PA", "Pennsylvania"},
                {"PR", "Puerto Rico"},
                {"RI", "Rhode Island"},
                {"SC", "South Carolina"},
                {"SD", "South Dakota"},
                {"TN", "Tennessee"},
                {"TX", "Texas"},
                {"UT", "Utah"},
                {"VA", "Virginia"},
                {"VI", "Virgin Islands"},
                {"VT", "Vermont"},
                {"WA", "Washington"},
                {"WI", "Wisconsin"},
                {"WV", "West Virginia"},
                {"WY", "Wyoming" }
            };
        }
    }
}
