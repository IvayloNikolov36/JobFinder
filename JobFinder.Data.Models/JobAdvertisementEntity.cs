namespace JobFinder.Data.Models
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Nomenclature;
    using System.ComponentModel.DataAnnotations;

    public class JobAdvertisementEntity : BaseEntity<int>
    {
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

        public JobCategoryEntity JobCategory { get; set; }

        public int JobEngagementId { get; set; }

        public JobEngagementEntity JobEngagement { get; set; }

        public int PublisherId { get; set; }

        public CompanyEntity Publisher { get; set; }

    }
}
