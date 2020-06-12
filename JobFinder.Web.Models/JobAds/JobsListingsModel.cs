namespace JobFinder.Web.Models.JobAds
{
    using System.Collections.Generic;

    public class JobsListingsModel
    {
        public int TotalCount { get; set; } //count of all filtered job ads

        public IEnumerable<JobListingModel> JobAds { get; set; } //filtered job ads for wanted page
    }
}
