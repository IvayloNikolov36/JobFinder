using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.CVModels
{
    public class SkillsEditModel : IMapTo<SkillsInfoEditDTO>
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
