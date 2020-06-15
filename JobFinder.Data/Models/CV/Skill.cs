namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Skill : BaseModel<int>
    {
        public Skill()
        {
            this.DrivingLicenseCategories = new HashSet<DrivingCategory>();
        }

        public string CurriculumVitaeId { get; set; }

        public CurriculumVitae CurriculumVitae { get; set; }

        [StringLength(10000, MinimumLength = 10)]
        public string ComputerSkills { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string Skills { get; set; }

        public bool HasManagedPeople { get; set; }

        public bool HasDrivingLicense { get; set; }

        public ICollection<DrivingCategory> DrivingLicenseCategories { get; set; }
    }
}
