using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public class GenderEntity : BaseEntity<int>
    {
        public GenderEntity()
        {
            this.PersonalInfos = new List<PersonalInfoEntity>();
        }

        public string Name { get; set; }

        public ICollection<PersonalInfoEntity> PersonalInfos { get; set; }
    }
}
