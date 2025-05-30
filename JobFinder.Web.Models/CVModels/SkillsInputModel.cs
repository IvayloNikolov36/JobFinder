using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.CVModels
{
    public class SkillsInputModel : IMapTo<SkillsInfoInputDTO>
    {
        [StringLength(10000, MinimumLength = 10)]
        public string ComputerSkills { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string Skills { get; set; }

        public bool HasManagedPeople { get; set; }

        public bool HasDrivingLicense { get; set; }

        public IEnumerable<int> DrivingLicenseCategoryIds { get; set; }
    }
}
