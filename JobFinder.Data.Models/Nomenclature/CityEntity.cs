using JobFinder.Data.Models.Common;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public partial class CityEntity : BaseNomenclatureEntity<int>
    {
        public CityEntity()
        {
            this.JobAdvertisements = new List<JobAdvertisementEntity>();
        }

        public ICollection<JobAdvertisementEntity> JobAdvertisements { get; set; }
    }
}
