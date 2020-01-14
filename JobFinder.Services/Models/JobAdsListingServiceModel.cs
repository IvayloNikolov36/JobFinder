using System;

namespace JobFinder.Services.Models
{
    public class JobAdsListingServiceModel
    {
        public string CompanyLogo { get; set; }

        public string Position { get; set; }

        public string Description { get; set; }

        public DateTime PostedOn { get; set; }

        public string JobCategory { get; set; }

        public string JobEngagement { get; set; }

    }
}
