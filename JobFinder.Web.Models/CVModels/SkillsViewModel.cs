namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Nomenclature;
    using JobFinder.Services.Mappings;
    using System.Collections.Generic;

    public class SkillsViewModel : IMapFrom<SkillsInfoEntity>
    {
        public int Id { get; set; }

        public string ComputerSkills { get; set; }

        public string Skills { get; set; }

        public bool HasManagedPeople { get; set; }

        public bool HasDrivingLicense { get; set; }

        // TODO: use viewModel, not the entity
        public ICollection<DrivingCategoryTypeEntity> DrivingLicenseCategories { get; set; }
    }
}
