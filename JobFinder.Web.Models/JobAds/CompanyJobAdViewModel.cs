using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using System;
using System.Linq;

namespace JobFinder.Web.Models.JobAds
{
    public class CompanyJobAdViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Position { get; set; }

        public string JobCategory { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public string Currency { get; set; }

        public string Location { get; set; }

        public int ApplicationsSent { get; set; }

        public int NotPreviewedApplications { get; set; }

        public bool IsActive { get; set; }

        public DateTime PublishDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdvertisementEntity, CompanyJobAdViewModel>()
                .ForMember(vm => vm.Location, o => o.MapFrom(e => e.Location.Name))
                .ForMember(vm => vm.ApplicationsSent, o => o.MapFrom(e => e.JobAdApplications.Count()))
                .ForMember(x => x.Currency, m => m.MapFrom(m => m.Currency.Name))
                .ForMember(vm => vm.NotPreviewedApplications, o => o.MapFrom(e => e
                    .JobAdApplications
                    .Where(ja => !ja.PreviewDate.HasValue)
                    .Count()));
        }
    }
}
