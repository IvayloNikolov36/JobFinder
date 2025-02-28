using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using System;

namespace JobFinder.Web.Models.JobAds
{
    public class JobApplicationInfoViewModel : IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Applicant { get; set; }

        public string CurriculumVitaeId { get; set; }

        public string CurriculumVitaeName { get; set; }

        public string CurriculumVitaePictureUrl { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime AppliedOn { get; set; }

        public bool IsPreviewed { get; set; }

        public DateTime? PreviewDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdApplicationEntity, JobApplicationInfoViewModel>()
                .ForMember(vm => vm.Applicant, o => o.MapFrom(e => e.Applicant.FirstName + " " + e.Applicant.LastName))
                .ForMember(vm => vm.Email, o => o.MapFrom(e => e.CurriculumVitae.PersonalDetails.Email))
                .ForMember(vm => vm.Phone, o => o.MapFrom(e => e.CurriculumVitae.PersonalDetails.Phone));
        }
    }
}
