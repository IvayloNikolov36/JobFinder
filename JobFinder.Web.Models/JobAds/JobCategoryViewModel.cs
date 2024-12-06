namespace JobFinder.Web.Models.JobAds
{
    using AutoMapper;
    using JobFinder.Data.Models.Nomenclature;
    using JobFinder.Services.Mappings;

    public class JobCategoryViewModel : IMapFrom<JobCategoryEntity>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobCategoryEntity, JobCategoryViewModel>()
               .ForMember(x => x.Name, m => m.MapFrom(m => m.Name));
        }
    }
}
