using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public class CitizenshipEntity : BaseEntity<int>
    {
        public CitizenshipEntity()
        {
            this.PersonalInfos = new List<PersonalInfoEntity>();
        }

        public string Name { get; set; }

        public ICollection<PersonalInfoEntity> PersonalInfos { get; set; }
    }
}
