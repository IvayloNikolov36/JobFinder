using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Subscriptions;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    // TODO: typo - rename everywhere
    public class ReccuringTypeEntity : BaseNomenclatureEntity<int>
    {
        public ReccuringTypeEntity()
        {
            this.JobSubscriptions = new List<JobsSubscriptionEntity>();
        }

        public ICollection<JobsSubscriptionEntity> JobSubscriptions { get; set; }
    }
}
