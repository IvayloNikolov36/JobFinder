namespace JobFinder.Data.Models.ViewsModels
{
    public class JobAdsSubscriptionsDbVewData
    {
        public int? JobCategoryId { get; set; }
        public string JobCategory { get; set; }

        public int? LocationId { get; set; }
        public string Location { get; set; }

        public string SubscribersEmails { get; set; }
    }
}
