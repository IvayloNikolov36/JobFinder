namespace JobFinder.Transfer.DTOs
{
    public class JobSubscriptionCriteriasDTO
    {
        public int RecurringTypeId { get; set; }

        public int? JobCategoryId { get; set; }

        public int? JobEngagementId { get; set; }

        public int? LocationId { get; set; }

        public bool Intership { get; set; }

        public bool SpecifiedSalary { get; set; }

        public string SearchTerm { get; set; }

        public string UserId { get; set; }
    }
}
