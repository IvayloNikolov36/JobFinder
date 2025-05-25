using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using System.Linq;

namespace JobFinder.Data.Models.CV;

public partial class CurriculumVitaeEntity : IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CurriculumVitaeEntity, AnonymousProfileCvDataDTO>()
            // TODO: remove the mapping after renaming to PersonalInfo
            .ForMember(dto => dto.PersonalInfo, o => o.MapFrom(e => e.PersonalDetails))
            .ForMember(dto => dto.WorkExperienceInfo, o => o.MapFrom(e => e.WorkExperiences
                .Where(we => we.IncludeInAnonymousProfile == true)))
            .ForMember(dto => dto.EducationInfo, o => o.MapFrom(e => e.Educations
                .Where(e => e.IncludeInAnonymousProfile == true)))
            .ForMember(dto => dto.LanguagesInfo, o => o.MapFrom(e => e.LanguagesInfo
                .Where(l => l.IncludeInAnonymousProfile == true)))
            .ForMember(dto => dto.CoursesInfo, o => o.MapFrom(e => e.CourseCertificates
                .Where(cs => cs.IncludeInAnonymousProfile == true)))
            .ForMember(dto => dto.SkillsInfo, o => o.MapFrom(e => e.Skills));
    }
}
