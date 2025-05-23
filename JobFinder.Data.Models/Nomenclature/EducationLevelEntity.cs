using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature;

public partial class EducationLevelEntity : BaseNomenclatureEntity<int>
{
    public EducationLevelEntity()
    {
        this.Educations = new List<EducationInfoEntity>();
    }

    public ICollection<EducationInfoEntity> Educations { get; set; }
}
