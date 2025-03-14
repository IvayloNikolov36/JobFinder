using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Subscriptions;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public class ReccuringTypeEntity : BaseNomenclatureEntity<int>
    {
        public ReccuringTypeEntity()
        {
            this.CompanySubscriptions = new List<CompanySubscriptionEntity>();
            this.JobSubscriptions = new List<JobsSubscriptionEntity>();
        }

        public ICollection<CompanySubscriptionEntity> CompanySubscriptions { get; set; }

        public ICollection<JobsSubscriptionEntity> JobSubscriptions { get; set; }
    }
}
