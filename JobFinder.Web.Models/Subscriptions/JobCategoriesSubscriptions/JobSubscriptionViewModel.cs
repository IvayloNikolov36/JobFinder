using AutoMapper;
using JobFinder.Data.Models.Subscriptions;
using JobFinder.Services.Mappings;

namespace JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions
{
    public class JobSubscriptionViewModel : IHaveCustomMappings
    {
        private const string NoPropValueSubstitution = "All";

        public int Id { get; set; }

        public string JobCategory { get; set; }

        public string Location { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobsSubscription, JobSubscriptionViewModel>()
                .ForMember(vm => vm.JobCategory, o => o.MapFrom(e => e.JobCategoryId.HasValue ? e.JobCategory.Name : NoPropValueSubstitution))
                .ForMember(vm => vm.Location, o => o.MapFrom(e => e.Location == null ? NoPropValueSubstitution : e.Location));
        }
    }
}
