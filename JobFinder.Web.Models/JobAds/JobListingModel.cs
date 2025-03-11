namespace JobFinder.Web.Models.JobAds
{
    using AutoMapper;
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;

    public class JobListingModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string CompanyLogo { get; set; }

        public string CompanyName { get; set; }

        public string Position { get; set; }

        public string PostedOn { get; set; }

        public string JobCategory { get; set; }

        public string JobEngagement { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public string Currency { get; set; }

        public string Location { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdvertisementEntity, JobListingModel>()
                .ForMember(x => x.CompanyId, m => m.MapFrom(m => m.PublisherId))
                .ForMember(x => x.CompanyLogo, m => m.MapFrom(m => m.Publisher.Logo))
                .ForMember(x => x.CompanyName, m => m.MapFrom(m => m.Publisher.Name))
                .ForMember(x => x.PostedOn, m => m.MapFrom(m => m.CreatedOn.ToString()))
                .ForMember(x => x.Currency, m => m.MapFrom(m => m.Currency.Name))
                .ForMember(x => x.JobEngagement, m => m.MapFrom(m => m.JobEngagement.Name))
                .ForMember(x => x.JobCategory, m => m.MapFrom(m => m.JobCategory.Name))
                .ForMember(x => x.Location, m => m.MapFrom(m => m.Location.Name));
        }

    }
}
