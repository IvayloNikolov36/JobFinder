namespace JobFinder.Web.Models.Subscriptions.CompanySubscriptions
{
    using AutoMapper;
    using JobFinder.Data.Models.Subscriptions;
    using JobFinder.Services.Mappings;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SubscribersNewJobAdsViewModel : IMapFrom<CompanySubscription>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogo { get; set; }

        public IList<JobAdsubscriptionViewModel> CompanyUserJobAds { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CompanySubscription, SubscribersNewJobAdsViewModel>()
                .ForMember(x => x.CompanyUserJobAds, m => m
                    .MapFrom(m => m.Company.JobAds
                        .Where(ja => (DateTime.UtcNow.Day - ja.CreatedOn.Date.Day) <= 1)));
        }
    }
}
