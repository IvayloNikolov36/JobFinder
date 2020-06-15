namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Education : BaseModel<int>
    {
        public string CurriculumVitaeId { get; set; }

        public CurriculumVitae CurriculumVitae { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Location { get; set; }

        public EducationLevel EducationLevel { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Major { get; set; }

        [StringLength(1000, MinimumLength = 4)]
        public string MainSubjects { get; set; }
    }
}
