﻿using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;

namespace JobFinder.Data.Models.ViewsModels
{
    public class JobAdsSubscriptionsDbFunctionResult : IMapTo<JobAdsSubscriptionDTO>
    {
        public int? JobCategoryId { get; set; }

        public int? JobEngagementId { get; set; }

        public int? LocationId { get; set; }

        public string SearchTerm { get; set; }

        public bool Intership { get; set; }

        public bool SpecifiedSalary { get; set; }

        public string SubscribersEmails { get; set; }
    }
}
