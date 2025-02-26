using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using System;

namespace JobFinder.Web.Models.JobAds
{
    public class JobAdApplicationViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public int JobAdId { get; set; }

        public string CurriculumVitaeId { get; set; }

        public string CurriculumVitaeName { get; set; }

        public string JobTitle { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogo { get; set; }

        public DateTime AppliedOn { get; set; }

        public bool IsPreviewed { get; set; }

        public DateTime? PreviewDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdApplicationEntity, JobAdApplicationViewModel>()
                .ForMember(vm => vm.CompanyLogo, o => o.MapFrom(e => e.JobAd.Publisher.Logo))
                .ForMember(vm => vm.CompanyName, o => o.MapFrom(e => e.JobAd.Publisher.Name))
                .ForMember(vm => vm.JobTitle, o => o.MapFrom(e => e.JobAd.Position));
        }
    }
}
