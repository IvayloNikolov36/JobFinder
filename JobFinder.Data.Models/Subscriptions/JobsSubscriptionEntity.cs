using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Nomenclature;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.Subscriptions
{
    public partial class JobsSubscriptionEntity : BaseEntity<int>
    {
        public int RecurringTypeId { get; set; }
        public RecurringTypeEntity RecurringType { get; set; }

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

        public string SearchTerm { get; set; }
    }
}
