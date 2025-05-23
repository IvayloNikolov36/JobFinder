﻿using JobFinder.Data.Models.Subscriptions;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions
{
    public class JobSubscriptionCriteriasViewModel : IMapTo<JobsSubscriptionEntity>, IMapTo<JobSubscriptionCriteriasDTO>
    {
        [Range(1, int.MaxValue)]
        public int RecurringTypeId { get; set; }

        [Range(1, int.MaxValue)]
        public int? JobCategoryId { get; set; }

        [Range(1, int.MaxValue)]
        public int? JobEngagementId { get; set; }

        [Range(1, int.MaxValue)]
        public int? LocationId { get; set; }

        public bool Intership { get; set; }

        public bool SpecifiedSalary { get; set; }

        public string SearchTerm { get; set; }
    }
}
