using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Cv;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature;

public partial class CountryEntity : BaseNomenclatureEntity<int>
{
    public CountryEntity()
    {
        this.PersonalInfos = new List<PersonalInfoEntity>();
    }

    public ICollection<PersonalInfoEntity> PersonalInfos { get; set; }
}
