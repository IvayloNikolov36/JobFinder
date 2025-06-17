using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Cv;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public partial class GenderEntity : BaseNomenclatureEntity<int>
    {
        public GenderEntity()
        {
            this.PersonalInfos = new List<PersonalInfoEntity>();
        }

        public ICollection<PersonalInfoEntity> PersonalInfos { get; set; }
    }
}
