using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Web.Models.JobAds
{
    public class JobAdDetailsViewModel : IMapFrom<JobAdDetailsDTO>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Position { get; set; }

        public string Desription { get; set; }

        public string Location { get; set; }

        public string PostedOn { get; set; }

        public string JobEngagement { get; set; }

        public string CompanyLogo { get; set; }

        public string CompanyName { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public string Currency { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdEntity, JobAdDetailsViewModel>()
                .ForMember(x => x.CompanyLogo, m => m.MapFrom(m => m.Publisher.Logo))
                .ForMember(x => x.CompanyName, m => m.MapFrom(m => m.Publisher.Name))
                .ForMember(x => x.PostedOn, m => m.MapFrom(m => m.CreatedOn.ToString()))
                .ForMember(x => x.Currency, m => m.MapFrom(m => m.Currency.Name))
                .ForMember(x => x.JobEngagement, m => m.MapFrom(m => m.JobEngagement.Name));
        }
    }
}
