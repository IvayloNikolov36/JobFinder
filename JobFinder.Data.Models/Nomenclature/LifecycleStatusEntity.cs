using JobFinder.Data.Models.Common;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature;

public class LifecycleStatusEntity : BaseNomenclatureEntity<int>
{
    public LifecycleStatusEntity()
    {
        this.JobAds = new List<JobAdEntity>();
    }

    public ICollection<JobAdEntity> JobAds { get; set; }
}
