namespace JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions
{
    using JobFinder.Data.Models.ViewsModels;
    using System.Collections.Generic;

    public class JobAdsByCategoryAndLocationViewModel
    {
        public int JobCategoryId { get; set; }

        public string JobCategory { get; set; }

        public string Location { get; set; }

        public string[] Subscribers { get; set; }

        public List<LatestCompanyJobAds> LatestCompanyJobAds { get; set; }
    }
}
