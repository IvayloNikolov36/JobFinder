namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Nomenclature;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WorkExperienceInfoEntity : BaseEntity<int>
    {
        [Required]
        public string CurriculumVitaeId { get; set; }

        public CurriculumVitaeEntity CurriculumVitae { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 4)]
        public string Organization { get; set; }

        [Required]
        public int BusinessSectorId { get; set; }
        public BusinessSectorEntity BusinessSector { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Location { get; set; }

        [StringLength(3000, MinimumLength = 20)]
        public string AditionalDetails { get; set; }
    }
}
