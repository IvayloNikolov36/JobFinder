using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using JobFinder.Web.Models.Company;

namespace JobFinder.Web.Models.Subscriptions
{
    public class JobAdDetailsForSubscriber : IHaveCustomMappings
    {
        public int Id { get; set; }

        public CompanyBasicViewModel Company { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public string JobCategoryName { get; set; }

        public string JobEngagementName { get; set; }

        public string Salary { get; set; }

        public bool Intership { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdvertisementEntity, JobAdDetailsForSubscriber>()
                .ForMember(vm => vm.Salary, o => o.MapFrom(e => e.MinSalary.HasValue
                    ? $"{e.MinSalary}-{e.MaxSalary} {e.Currency.Name}"
                    : null))
                .ForMember(vm => vm.Location, o => o.MapFrom(e => e.Location.Name))
                .ForMember(vm => vm.Company, o => o.MapFrom(e => e.Publisher));
        }
    }
}
