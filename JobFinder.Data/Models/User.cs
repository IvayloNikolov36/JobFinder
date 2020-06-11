using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.JobApplications = new List<JobApplication>();
            this.JobAds = new List<JobAd>();
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

        public IList<JobApplication> JobApplications { get; set; }

        public IList<JobAd> JobAds { get; set; }
    }
}
