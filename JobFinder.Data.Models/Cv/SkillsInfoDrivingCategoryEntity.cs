using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Nomenclature;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.Cv
{
    public class SkillsInfoDrivingCategoryEntity : BaseEntity<int>
    {
        [Required]
        public int SkillsInfoId { get; set; }
        public SkillsInfoEntity SkillsInfo { get; set; }

        [Required]
        public int DrivingCategoryId { get; set; }
        public DrivingCategoryEntity DrivingCategory { get; set; }
    }
}
