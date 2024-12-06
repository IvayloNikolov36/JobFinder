namespace JobFinder.Web.Models.JobAds
{
    using AutoMapper;
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;

    public class JobAdDetailsModel : IMapFrom<JobAdvertisementEntity>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Position { get; set; }

        public string Desription { get; set; }

        public string Location { get; set; }

        public string PostedOn { get; set; }

        public string JobEngagement { get; set; }

        public string CompanyLogo { get; set; }

        public string CompanyName { get; set; }

        public string Salary { get; set; } //diapason

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdvertisementEntity, JobAdDetailsModel>()
                .ForMember(x => x.CompanyLogo, m => m.MapFrom(m => m.Publisher.Logo))
                .ForMember(x => x.CompanyName, m => m.MapFrom(m => m.Publisher.Name))
                .ForMember(x => x.PostedOn, m => m.MapFrom(m => m.CreatedOn.ToString()))
                .ForMember(x => x.Salary, m => m.MapFrom(
                    m => m.MinSalary.ToString() + " - " + m.MaxSalary.ToString()))
                .ForMember(x => x.JobEngagement, m => m.MapFrom(m => m.JobEngagement.Name));
        }
    }
}
