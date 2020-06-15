namespace JobFinder.Data.Models
{
    using JobFinder.Data.Models.CV;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        public User()
        {        
            this.CVs = new HashSet<CurriculumVitae>();
            this.JobApplications = new HashSet<JobApplication>();
            this.JobAds = new HashSet<JobAd>();
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

        public ICollection<JobApplication> JobApplications { get; set; }

        public ICollection<JobAd> JobAds { get; set; }

    }
}
