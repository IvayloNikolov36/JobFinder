using System.Collections.Generic;

namespace JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions
{   
    public class JobAdsSubscriptionsViewModel
    {
        public required string JobCategory { get; set; }

        public required string JobEngagement { get; set; }

        public required string Location { get; set; }

        public required string SearchTerm { get; set; }

        public required bool SpecifiedSalary { get; set; }

        public required bool Intership { get; set; }

        public required IEnumerable<JobAdDetailsForSubscriber> JobAds { get; set; }
    }
}
