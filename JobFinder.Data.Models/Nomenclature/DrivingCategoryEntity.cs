using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Cv;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public class DrivingCategoryEntity : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<SkillsInfoDrivingCategoryEntity> SkillsInfoDrivingCategories { get; set; }
    }
}
