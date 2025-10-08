using JobFinder.Data.Models.Cv;
using JobFinder.Data.Models.Subscriptions;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models
{
    public partial class UserEntity : IdentityUser
    {
        public UserEntity()
        {        
            this.Cvs = new List<CurriculumVitaeEntity>();
            this.JobCategorySubscriptions = new List<JobsSubscriptionEntity>();
            this.CompanySubscriptions = new List<CompanySubscriptionEntity>();
            this.JobAdApplications = new List<JobAdApplicationEntity>();
            this.UploadedImages = new List<CloudImageEntity>();
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

        public ICollection<CurriculumVitaeEntity> Cvs { get; set; }

        public ICollection<CompanySubscriptionEntity> CompanySubscriptions { get; set; }

        public ICollection<JobsSubscriptionEntity> JobCategorySubscriptions { get; set; }

        public ICollection<JobAdApplicationEntity> JobAdApplications { get; set; }

        public ICollection<CloudImageEntity> UploadedImages { get; set; }
    }
}
