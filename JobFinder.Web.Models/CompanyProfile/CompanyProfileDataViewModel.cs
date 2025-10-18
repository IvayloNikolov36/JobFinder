using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using System.Linq;

namespace JobFinder.Web.Models.CompanyProfile;

public class CompanyProfileDataViewModel : IHaveCustomMappings
{
    public string LogoThumbnailUrl { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public int ActiveAdsCount { get; set; }

    public int NewApplicationsCount { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CompanyProfileDataDTO, CompanyProfileDataViewModel>()
            .ForMember(vm => vm.NewApplicationsCount, o => o.MapFrom(e => e.NewApplications.Sum()));
    }
}
