﻿using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Subscriptions;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Nomenclature
{
    public partial class RecurringTypeEntity : BaseNomenclatureEntity<int>
    {
        public RecurringTypeEntity()
        {
            this.JobSubscriptions = new List<JobsSubscriptionEntity>();
        }

        public ICollection<JobsSubscriptionEntity> JobSubscriptions { get; set; }
    }
}
