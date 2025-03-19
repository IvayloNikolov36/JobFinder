namespace JobFinder.Web.Models.JobAds
{
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;
    using System.ComponentModel.DataAnnotations;

    public class JobAdCreateModel : IMapTo<JobAdvertisementEntity>
    {
        [Required]
        [StringLength(90, MinimumLength = 6)]
        public string Position { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public int LocationId { get; set; }

        public int JobCategoryId { get; set; }

        public int JobEngagementId { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public int? CurrencyId { get; set; }

        public bool Intership { get; set; }
    }
}
