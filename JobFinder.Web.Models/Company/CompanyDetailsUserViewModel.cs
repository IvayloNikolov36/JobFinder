using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using System.Linq;

namespace JobFinder.Web.Models.Company
{
    public class CompanyDetailsUserViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public int ActiveAdsCount { get; set; }

        public bool HasSubscription { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            string currentUserId = string.Empty;

            configuration
                .CreateMap<CompanyEntity, CompanyDetailsUserViewModel>()
                .ForMember(vm => vm.ActiveAdsCount, o => o.MapFrom(e => e.JobAds.Where(ja => ja.IsActive).Count()))
                .ForMember(vm => vm.HasSubscription, o => o.MapFrom(e => e.CompanySubscriptions.Any(cs => cs.UserId == currentUserId)));
        }
    }
}
