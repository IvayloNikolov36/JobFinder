namespace JobFinder.Data.Models
{
    using JobFinder.Data.Models.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class JobAd : BaseModel<int>
    {
        public JobAd()
        {
            this.JobApplications = new HashSet<JobApplication>();
        }

        [Required]
        [StringLength(90, MinimumLength = 6)]
        public string Position { get; set; }

        [Required]
        [MinLength(20)]
        public string Desription { get; set; }

        [Required]
        public string Location { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public int JobCategoryId { get; set; }

        public JobCategory JobCategory { get; set; }

        public int JobEngagementId { get; set; }

        public JobEngagement JobEngagement { get; set; }

        public string PublisherId { get; set; }

        public User Publisher { get; set; }

        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
