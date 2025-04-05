using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;

namespace JobFinder.Data.Models.Subscriptions
{
    public partial class JobsSubscriptionEntity : IMapFrom<JobSubscriptionCriteriasDTO>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobsSubscriptionEntity, JobSubscriptionDTO>()
                .ForMember(vm => vm.RecurringType, o => o.MapFrom(e => e.RecurringType.Name))
                .ForMember(vm => vm.JobCategory, o => o.MapFrom(e => e.JobCategory.Name))
                .ForMember(vm => vm.JobEngagement, o => o.MapFrom(e => e.JobEngagement.Name))
                .ForMember(vm => vm.Location, o => o.MapFrom(e => e.Location.Name));
        }
    }
}
