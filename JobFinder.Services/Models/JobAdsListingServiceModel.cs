﻿using System;

namespace JobFinder.Services.Models
{
    public class JobAdsListingServiceModel
    {
        public int Id { get; set; }

        public string CompanyLogo { get; set; }

        public string CompanyName { get; set; }

        public string Position { get; set; }

        public string PostedOn { get; set; }

        public string JobCategory { get; set; }

        public string JobEngagement { get; set; }

        public string Salary { get; set; }

        public string Location { get; set; }

    }
}