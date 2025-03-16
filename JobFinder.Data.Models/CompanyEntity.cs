namespace JobFinder.Data.Models
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Subscriptions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CompanyEntity : BaseEntity<int>
    {
        public CompanyEntity()
        {
            this.JobAds = new List<JobAdvertisementEntity>();
            this.CompanySubscriptions = new List<CompanySubscriptionEntity>();
        }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Name { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 9)]
        public string Bulstat { get; set; }

        public string UserId { get; set; }

        public UserEntity User { get; set; }

        public ICollection<JobAdvertisementEntity> JobAds { get; set; }

        public ICollection<CompanySubscriptionEntity> CompanySubscriptions { get; set; }
    }
}
