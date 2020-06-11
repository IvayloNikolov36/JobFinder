using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models
{
    public class JobAd
    {
        public JobAd()
        {
            this.JobApplications = new List<JobApplication>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(90, MinimumLength = 6)]
        public string Position { get; set; }

        [Required]
        [MinLength(20)]
        public string Desription { get; set; }

        [Required]
        public string Location { get; set; }

        public DateTime PostedOn { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public int JobCategoryId { get; set; }

        public JobCategory JobCategory { get; set; }

        public int JobEngagementId { get; set; }

        public JobEngagement JobEngagement { get; set; }

        public string PublisherId { get; set; }

        public User Publisher { get; set; }

        public IList<JobApplication> JobApplications { get; set; }
    }
}
