using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Cv;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature;

public partial class DrivingCategoryEntity : BaseNomenclatureEntity<int>
{
    public ICollection<SkillsInfoDrivingCategoryEntity> SkillsInfoDrivingCategories { get; set; }
}
