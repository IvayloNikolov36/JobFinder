using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.Data.Models;

public partial class JobAdApplicationEntity : IMapFrom<JobAddApplicationInputDTO>, IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<JobAdApplicationEntity, JobAdApplicationDTO>()
            .ForMember(dto => dto.CompanyLogo, o => o.MapFrom(e => e.JobAd.Publisher.Logo))
            .ForMember(dto => dto.CompanyName, o => o.MapFrom(e => e.JobAd.Publisher.Name))
            .ForMember(dto => dto.JobTitle, o => o.MapFrom(e => e.JobAd.Position));

        configuration.CreateMap<JobAdApplicationEntity, JobAdApplicationInfoDTO>()
            .ForMember(dto => dto.Applicant, o => o
                .MapFrom(e => e.Applicant.FirstName + " " + e.Applicant.LastName))
            .ForMember(dto => dto.Email, o => o.MapFrom(e => e.CurriculumVitae.PersonalInfo.Email))
            .ForMember(dto => dto.Phone, o => o.MapFrom(e => e.CurriculumVitae.PersonalInfo.Phone));
    }
}
