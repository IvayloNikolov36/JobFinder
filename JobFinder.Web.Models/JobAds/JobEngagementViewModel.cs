namespace JobFinder.Web.Models.JobAds
{
    using AutoMapper;
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;

    public class JobEngagementViewModel : IMapFrom<JobEngagement>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobEngagement, JobEngagementViewModel>()
               .ForMember(x => x.Name, m => m.MapFrom(m => m.Name));
        }
    }
}
