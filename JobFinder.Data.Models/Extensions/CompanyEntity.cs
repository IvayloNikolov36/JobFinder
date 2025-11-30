using AutoMapper;
using JobFinder.Data.Models.Enums;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using System;
using System.Linq;

namespace JobFinder.Data.Models;

public partial class CompanyEntity : IMapTo<CompanyBasicDTO>,
    IMapTo<CompanyBasicDetailsDTO>,
    IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CompanyEntity, CompanyProfileDataDTO>()
            .ForMember(dto => dto.Email, o => o.MapFrom(e => e.User.Email))
            .ForMember(dto => dto.ActiveAdsCount, o => o.MapFrom(e => e.JobAds
                .Where(ja => ja.LifecycleStatusId == (int)LifecycleStatusEnum.Active)
                .Count()))
            .ForMember(dto => dto.NewApplications, o => o.MapFrom(e => e.JobAds
                .Where(j => j.LifecycleStatusId == (int)LifecycleStatusEnum.Active)
                .Select(j => j.JobAdApplications
                    .Where(a => !a.PreviewDate.HasValue)
                    .Count())
                )
            );

        string currentUserId = string.Empty;

        configuration
            .CreateMap<CompanyEntity, CompanyDetailsUserDTO>()
            .ForMember(dto => dto.ActiveAdsCount, o => o.MapFrom(e => e.JobAds
                .Where(ja => ja.LifecycleStatusId == (int)LifecycleStatusEnum.Active)
                .Count()))
            .ForMember(dto => dto.HasSubscription, o => o.MapFrom(e => e.CompanySubscriptions
                .Any(cs => cs.UserId == currentUserId))
            );

        configuration.CreateMap<CompanyEntity, CompanyJobAdsListingDTO>()
            .ForMember(dto => dto.CompanyDetails, o => o.MapFrom(e => e))
            .ForMember(dto => dto.Ads, o => o.MapFrom(e => e.JobAds
                .Where(a => a.LifecycleStatusId == (int)LifecycleStatusEnum.Active)
                .OrderByDescending(a => a.PublishDate))
            );

        string userId = null;

        configuration.CreateMap<CompanyEntity, CompanyListingDTO>()
            .ForMember(dto => dto.Ads, o => o.MapFrom(e => e.JobAds.Count()))
            .ForMember(dto => dto.Subscription, o => o.MapFrom(e => e.CompanySubscriptions
                .Any(cs => cs.UserId == userId)))
            .ForMember(dto => dto.LogoId, o => o.MapFrom(e => e.LogoImageId));
    }
}
