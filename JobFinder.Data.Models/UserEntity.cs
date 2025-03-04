namespace JobFinder.Data.Models
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Subscriptions;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserEntity : IdentityUser
    {
        public UserEntity()
        {        
            this.CurriculumVitaes = new List<CurriculumVitaeEntity>();
            this.JobCategorySubscriptions = new List<JobsSubscription>();
            this.CompanySubscriptions = new List<CompanySubscriptionEntity>();
            this.JobAdApplications = new List<JobAdApplicationEntity>();
        }

        [Required]
        [StringLength(
            25,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(
            25,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 2)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(
            25,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 2)]
        public string LastName { get; set; }

        public CompanyEntity Company { get; set; }

        public ICollection<CurriculumVitaeEntity> CurriculumVitaes { get; set; }

        public ICollection<CompanySubscriptionEntity> CompanySubscriptions { get; set; }

        public ICollection<JobsSubscription> JobCategorySubscriptions { get; set; }

        public ICollection<JobAdApplicationEntity> JobAdApplications { get; set; }
    }
}
