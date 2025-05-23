using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public partial class CitizenshipEntity : BaseNomenclatureEntity<int>
    {
        public CitizenshipEntity()
        {
            this.PersonalInfos = new List<PersonalInfoEntity>();
        }

        public ICollection<PersonalInfoEntity> PersonalInfos { get; set; }
    }
}
