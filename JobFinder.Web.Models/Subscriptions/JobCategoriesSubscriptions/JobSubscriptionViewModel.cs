using AutoMapper;
using JobFinder.Data.Models.Subscriptions;
using JobFinder.Services.Mappings;

namespace JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions
{
    public class JobSubscriptionViewModel : IHaveCustomMappings
    {
        private const string NoPropValueSubstitution = "All";

        public int Id { get; set; }

        public string ReccuringType { get; set; }

        public string JobCategory { get; set; }

        public string JobEngagement { get; set; }

        public string Location { get; set; }

        public bool SpecifiedSalary { get; set; }

        public bool Intership { get; set; }

        public string SearchTerm { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobsSubscriptionEntity, JobSubscriptionViewModel>()
                .ForMember(vm => vm.ReccuringType, o => o.MapFrom(e => e.ReccuringType.Name))
                .ForMember(vm => vm.JobCategory, o => o.MapFrom(e => e.JobCategoryId.HasValue ? e.JobCategory.Name : NoPropValueSubstitution))
                .ForMember(vm => vm.JobEngagement, o => o.MapFrom(e => e.JobEngagementId.HasValue ? e.JobEngagement.Name : NoPropValueSubstitution))
                .ForMember(vm => vm.Location, o => o.MapFrom(e => e.LocationId.HasValue ? e.Location.Name : NoPropValueSubstitution));
        }
    }
}
