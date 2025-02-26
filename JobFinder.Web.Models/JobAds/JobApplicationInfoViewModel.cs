using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using System;

namespace JobFinder.Web.Models.JobAds
{
    public class JobApplicationInfoViewModel : IHaveCustomMappings
    {
        public string Applicant { get; set; }

        public string CurriculumVitaeId { get; set; }

        public string CurriculumVitaeName { get; set; }

        public DateTime AppliedOn { get; set; }

        public bool IsPreviewed { get; set; }

        public DateTime? PreviewDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdApplicationEntity, JobApplicationInfoViewModel>()
                .ForMember(vm => vm.Applicant, o => o.MapFrom(e => e.Applicant.FirstName + " " + e.Applicant.LastName));
        }
    }
}
