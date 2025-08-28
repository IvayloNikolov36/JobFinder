using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Web.Models.JobAds
{
    public class JobAdCreateViewModel : JobAdBaseViewModel,
        IMapTo<JobAdCreateDTO>,
        IMapTo<JobAdCategoryDTO>,
        IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdCreateViewModel, SalaryPropertiesDTO>()
                .ForMember(dto => dto.HasCurrencyType, o => o.MapFrom(vm => vm.CurrencyId.HasValue));
        }
    }
}
