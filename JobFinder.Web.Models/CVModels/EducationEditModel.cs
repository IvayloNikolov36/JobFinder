using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.CvModels
{
    public class EducationEditModel : IMapTo<EducationInfoEditDTO>
    {
        public int Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Organization { get; set;}

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Location { get; set; }

        [Required]
        public int EducationLevelId { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Major { get; set; }

        [StringLength(1000, MinimumLength = 4)]
        public string MainSubjects { get; set; }
    }
}
