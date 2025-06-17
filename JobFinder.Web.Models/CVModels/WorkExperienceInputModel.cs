using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.CvModels
{
    public class WorkExperienceInputModel : IMapTo<WorkExperienceInfoInputDTO>
    {
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
