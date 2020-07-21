namespace JobFinder.Data.Models
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Subscriptions;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        public User()
        {        
            this.CVs = new HashSet<CurriculumVitae>();
            this.JobCategorySubscriptions = new HashSet<JobCategorySubscription>();
            this.CompanySubscriptions = new HashSet<CompanySubscription>();
        }

        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        public Company Company { get; set; }

        public ICollection<CurriculumVitae> CVs { get; set; }

        public ICollection<CompanySubscription> CompanySubscriptions { get; set; }

        public ICollection<JobCategorySubscription> JobCategorySubscriptions { get; set; }

    }
}
