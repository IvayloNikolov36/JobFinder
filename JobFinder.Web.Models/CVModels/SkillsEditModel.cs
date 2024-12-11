namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SkillsEditModel : IMapTo<SkillsInfoEntity>
    {
        public int Id { get; set; }

        [StringLength(10000, MinimumLength = 10)]
        public string ComputerSkills { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string OtherSkills { get; set; }

        public bool HasManagedPeople { get; set; }

        public IEnumerable<int> DrivingLicenseCategoryIds { get; set; }
    }
}
