using JobFinder.Data.Models.Common;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public class LanguageTypeEntity : BaseNomenclatureEntity<int>
    {
        public LanguageTypeEntity()
        {
            this.LanguageTypes = new List<LanguageTypeEntity>();
        }

        public ICollection<LanguageTypeEntity> LanguageTypes { get; set; }
    }
}
