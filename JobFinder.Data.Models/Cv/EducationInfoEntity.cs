using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Nomenclature;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.CV
{
    public partial class EducationInfoEntity : BaseEntity<int>
    {
        [Required]
        public string CurriculumVitaeId { get; set; }
        public CurriculumVitaeEntity CurriculumVitae { get; set; }

        public bool? IncludeInAnonymousProfile { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Organization { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Location { get; set; }

        [Required]
        public int EducationLevelId { get; set; }
        public EducationLevelEntity EducationLevel { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Major { get; set; }

        [StringLength(1000, MinimumLength = 4)]
        public string MainSubjects { get; set; }
    }
}
