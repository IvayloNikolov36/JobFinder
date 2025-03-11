using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public class GenderEntity : BaseNomenclatureEntity<int>
    {
        public GenderEntity()
        {
            this.PersonalInfos = new List<PersonalInfoEntity>();
        }

        public ICollection<PersonalInfoEntity> PersonalInfos { get; set; }
    }
}
