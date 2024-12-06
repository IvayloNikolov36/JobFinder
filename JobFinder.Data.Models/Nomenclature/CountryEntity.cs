using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public class CountryEntity : BaseEntity<int>
    {
        public CountryEntity()
        {
            this.PersonalInfos = new List<PersonalInfoEntity>();
        }

        public string Name { get; set; }

        public ICollection<PersonalInfoEntity> PersonalInfos { get; set; }
    }
}
