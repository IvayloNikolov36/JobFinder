namespace JobFinder.Data.Models.ViewsModels
{
    public class JobAdsSubscriptionsDbVewData
    {
        public int ReccuringTypeId { get; set; }
        public string ReccuringType { get; set; }

        public int? JobCategoryId { get; set; }
        public string JobCategory { get; set; }

        public int? JobEngagementId { get; set; }
        public string JobEngagement { get; set; }

        public int? LocationId { get; set; }
        public string Location { get; set; }

        public string SearchTerm { get; set; }

        public bool Intership { get; set; }

        public bool SpecifiedSalary { get; set; }

        public string SubscribersEmails { get; set; }
    }
}
