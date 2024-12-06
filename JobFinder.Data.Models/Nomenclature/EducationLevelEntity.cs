using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public class EducationLevelEntity : BaseEntity<int>
    {
        public EducationLevelEntity()
        {
            this.Educations = new List<EducationInfoEntity>();
        }

        public string Name { get; set; }

        public ICollection<EducationInfoEntity> Educations { get; set; }
    }
}
