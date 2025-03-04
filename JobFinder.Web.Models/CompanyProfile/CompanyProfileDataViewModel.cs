using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using System.Linq;
using System.Text.Json.Serialization;

namespace JobFinder.Web.Models.CompanyProfile
{
    public class CompanyProfileDataViewModel : IHaveCustomMappings
    {
        public string Logo { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int ActiveAdsCount { get; set; }

        [JsonIgnore]
        public int[] NewApplications { get; set; }

        public int NewApplicationsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CompanyEntity, CompanyProfileDataViewModel>()
                .ForMember(vm => vm.Email, o => o.MapFrom(e => e.User.Email))
                .ForMember(vm => vm.ActiveAdsCount, o => o.MapFrom(e => e.JobAds.Where(ja => ja.IsActive).Count()))
                .ForMember(vm => vm.NewApplications, o => o.MapFrom(e => e.JobAds
                    .Where(j => j.IsActive)
                    .Select(j => j.JobAdApplications
                        .Where(a => !a.PreviewDate.HasValue)
                        .Count())
                    )
                );
        }
    }
}
