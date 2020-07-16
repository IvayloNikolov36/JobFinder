namespace JobFinder.Web.Models.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WorkExperienceEditModel
    {
        public DateTime FromDate { get; set; }

        //TODO: make validator Todate to be after FromDate
        public DateTime? ToDate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 4)]
        public string Organization { get; set; }

        public BusinessSector BusinessSector { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Location { get; set; }

        [StringLength(3000, MinimumLength = 20)]
        public string AditionalDetails { get; set; }
    }
}
