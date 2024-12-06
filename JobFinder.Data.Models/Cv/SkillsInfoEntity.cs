namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Nomenclature;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SkillsInfoEntity : BaseEntity<int>
    {
        public SkillsInfoEntity()
        {
            this.DrivingLicenseCategories = new List<DrivingCategoryTypeEntity>();
        }

        [Required]
        public string CurriculumVitaeId { get; set; }
        public CurriculumVitaeEntity CurriculumVitae { get; set; }

        [StringLength(10000, MinimumLength = 10)]
        public string ComputerSkills { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string OtherSkills { get; set; }

        public bool HasManagedPeople { get; set; }

        public bool HasDrivingLicense { get; set; }

        public ICollection<DrivingCategoryTypeEntity> DrivingLicenseCategories { get; set; }
    }
}
