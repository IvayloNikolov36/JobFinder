﻿namespace JobFinder.Web.Models.CVModels
{
    using System.ComponentModel.DataAnnotations;

    public class SkillsEditModel
    {
        [StringLength(10000, MinimumLength = 10)]
        public string ComputerSkills { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string Skills { get; set; }

        public bool HasManagedPeople { get; set; }

        public bool HasDrivingLicense { get; set; }

        //public ICollection<DrivingCategory> DrivingLicenseCategories { get; set; }
    }
}
