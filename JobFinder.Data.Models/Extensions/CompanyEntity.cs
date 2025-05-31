using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using System;
using System.Linq;

namespace JobFinder.Data.Models;

public partial class CompanyEntity : IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CompanyEntity, CompanyProfileDataDTO>()
            .ForMember(dto => dto.Email, o => o.MapFrom(e => e.User.Email))
            .ForMember(dto => dto.ActiveAdsCount, o => o.MapFrom(e => e.JobAds
                .Where(ja => ja.IsActive)
                .Count()))
            .ForMember(dto => dto.NewApplications, o => o.MapFrom(e => e.JobAds
                .Where(j => j.IsActive)
                .Select(j => j.JobAdApplications
                    .Where(a => !a.PreviewDate.HasValue)
                    .Count())
                )
            );

        string currentUserId = string.Empty;

        configuration
            .CreateMap<CompanyEntity, CompanyDetailsUserDTO>()
            .ForMember(dto => dto.ActiveAdsCount, o => o.MapFrom(e => e.JobAds
                .Where(ja => ja.IsActive)
                .Count()))
            .ForMember(dto => dto.HasSubscription, o => o.MapFrom(e => e.CompanySubscriptions
                .Any(cs => cs.UserId == currentUserId)));
    }
}
