using JobFinder.Data.Models.Common;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public class CityEntity : BaseEntity<int>
    {
        public CityEntity()
        {
            this.JobAdvertisements = new List<JobAdvertisementEntity>();
        }

        public string Name { get; set; }

        public ICollection<JobAdvertisementEntity> JobAdvertisements { get; set; }
    }
}
