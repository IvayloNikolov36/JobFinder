﻿using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Web.Models.JobAds
{
    public class JobAdsFilterModel : IMapTo<JobAdFilterDTO>
    {
        private const int InitialPage = 1;
        private const int DefaultItemsPerPage = 10;

        public int Page { get; set; } = InitialPage;

        public int Items { get; set; } = DefaultItemsPerPage;

        public string SearchText { get; set; } = string.Empty;

        public int? CategoryId { get; set; }

        public int? EngagementId { get; set; }

        public int? LocationId { get; set; }

        public string SortBy { get; set; }

        public bool IsAscending { get; set; } = false;

        public bool SpecifiedSalary { get; set; } = false;

        public bool Intership { get; set; } = false;
    }
}
