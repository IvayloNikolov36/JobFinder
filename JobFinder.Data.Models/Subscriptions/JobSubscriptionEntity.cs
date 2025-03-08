namespace JobFinder.Data.Models.Subscriptions
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Nomenclature;
    using System.ComponentModel.DataAnnotations;

    public class JobsSubscriptionEntity : BaseEntity<int>
    {
        [Required]
        public string UserId { get; set; }
        public UserEntity User { get; set; }

        public int? JobCategoryId { get; set; }
        public JobCategoryEntity JobCategory { get; set; }

        public int? LocationId { get; set; }
        public CityEntity Location { get; set; }
    }
}
