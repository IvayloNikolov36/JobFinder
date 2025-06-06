﻿using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Cv;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.CV
{
    public partial class SkillsInfoEntity : BaseEntity<int>
    {
        public SkillsInfoEntity()
        {
            this.SkillsInfoDrivingCategories = new List<SkillsInfoDrivingCategoryEntity>();
        }

        [Required]
        public string CurriculumVitaeId { get; set; }
        public CurriculumVitaeEntity CurriculumVitae { get; set; }

        [StringLength(10000, MinimumLength = 10)]
        public string ComputerSkills { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string OtherSkills { get; set; }

        public bool HasManagedPeople { get; set; }

        // TODO: consider removing this property
        public bool HasDrivingLicense { get; set; }

        public List<SkillsInfoDrivingCategoryEntity> SkillsInfoDrivingCategories { get; set; }
    }
}
