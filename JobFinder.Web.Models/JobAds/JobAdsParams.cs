namespace JobFinder.Web.Models.JobAds
{
    public class JobAdsParams
    {
        public int Page { get; set; } = 1;

        public int Items { get; set; } = 10;

        public string SearchText { get; set; }

        public int CategoryId { get; set; } = 0;

        public int EngagementId { get; set; } = 0;

        public string Location { get; set; }

        public string SortBy { get; set; }

        public bool IsAscending { get; set; }
    }
}
