using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public partial class BusinessSectorEntity : BaseNomenclatureEntity<int>
    {
        public BusinessSectorEntity()
        {
            this.WorkExperiences = new List<WorkExperienceInfoEntity>();
        }

        public ICollection<WorkExperienceInfoEntity> WorkExperiences { get; set; }
    }
}
