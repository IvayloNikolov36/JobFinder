﻿namespace JobFinder.Web.Models.CurriculumVitae
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using System.Collections.Generic;

    public class SkillsViewModel : IMapFrom<Skill>
    {
        public string CurriculumVitaeId { get; set; }

        public string ComputerSkills { get; set; }

        public string Skills { get; set; }

        public bool HasManagedPeople { get; set; }

        public ICollection<DrivingCategory> DrivingLicenseCategories { get; set; }
    }
}
