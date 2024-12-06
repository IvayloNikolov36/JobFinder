using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public class BusinessSectorEntity : BaseEntity<int>
    {
        public BusinessSectorEntity()
        {
            this.WorkExperiences = new List<WorkExperienceInfoEntity>();
        }

        public string Name { get; set; }

        public ICollection<WorkExperienceInfoEntity> WorkExperiences { get; set; }
    }
}
