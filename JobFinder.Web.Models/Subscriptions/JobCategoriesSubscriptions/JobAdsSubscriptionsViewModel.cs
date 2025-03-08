namespace JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions
{
    using JobFinder.Data.Models.ViewsModels;
    using System.Collections.Generic;

    public class JobAdsSubscriptionsViewModel
    {
        public int? JobCategoryId { get; set; }
        public string JobCategory { get; set; }

        public int? LocationId { get; set; }
        public string Location { get; set; }

        public string[] Subscribers { get; set; }

        public List<LatestJobAdsDbFunctionResult> LatestJobAds { get; set; }
    }
}
