﻿using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Nomenclature;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.Cv
{
    public partial class WorkExperienceInfoEntity : BaseEntity<int>
    {
        [Required]
        public string CvId { get; set; }

        public CurriculumVitaeEntity Cv { get; set; }

        public bool? IncludeInAnonymousProfile { get; set; }

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
        public string AdditionalDetails { get; set; }
    }
}
