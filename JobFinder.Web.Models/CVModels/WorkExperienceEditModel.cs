namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WorkExperienceEditModel : IMapTo<WorkExperienceInfoEntity>
    {
        public int Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 4)]
        public string Organization { get; set; }

        public int BusinessSectorId { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Location { get; set; }

        [StringLength(3000, MinimumLength = 20)]
        public string AdditionalDetails { get; set; }
    }
}
