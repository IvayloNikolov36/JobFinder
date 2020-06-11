namespace JobFinder.Web.Models.JobAds
{
    using System.ComponentModel.DataAnnotations;

    public class JobAdBindingModel
    {
        [Required]
        [StringLength(90, MinimumLength = 6)]
        public string Position { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Location { get; set; }

        public int JobCategoryId { get; set; }

        public int JobEngagementId { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }
    }
}
