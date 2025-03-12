namespace JobFinder.Data.Models.Subscriptions
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Nomenclature;
    using System.ComponentModel.DataAnnotations;

    public class JobsSubscriptionEntity : BaseEntity<int>
    {
        public int ReccuringTypeId { get; set; }
        public ReccuringTypeEntity ReccuringType { get; set; }

        [Required]
        public string UserId { get; set; }
        public UserEntity User { get; set; }

        public int? JobCategoryId { get; set; }
        public JobCategoryEntity JobCategory { get; set; }

        public int? JobEngagementId { get; set; }
        public JobEngagementEntity JobEngagement { get; set; }

        public int? LocationId { get; set; }
        public CityEntity Location { get; set; }

        public bool Intership { get; set; }

        public bool SpecifiedSalary { get; set; }

        public string SearchTerm { get; set; } = null;
    }
}
