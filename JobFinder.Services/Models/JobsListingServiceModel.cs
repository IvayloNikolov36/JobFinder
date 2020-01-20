using System.Collections.Generic;

namespace JobFinder.Services.Models
{
    public class JobsListingServiceModel
    {
        public int TotalCount { get; set; } //count of all filtered job ads

        public IEnumerable<JobListingModel> JobAds { get; set; } //filtered job ads for wanted page
    }
}
