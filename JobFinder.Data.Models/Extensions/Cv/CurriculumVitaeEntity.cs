using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.CV;
using System.Linq;

namespace JobFinder.Data.Models.CV;

public partial class CurriculumVitaeEntity :
    IHaveCustomMappings
{
    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<CVCreateDTO, CurriculumVitaeEntity>()
            .ForMember(e => e.Id, o => o.Ignore());

        configuration.CreateMap<CurriculumVitaeEntity, AnonymousProfileDataDTO>()
            .ForMember(dto => dto.WorkExperienceInfo, o => o.MapFrom(e => e.WorkExperiences
                .Where(we => we.IncludeInAnonymousProfile == true)))
            .ForMember(dto => dto.EducationInfo, o => o.MapFrom(e => e.Educations
                .Where(e => e.IncludeInAnonymousProfile == true)))
            .ForMember(dto => dto.LanguagesInfo, o => o.MapFrom(e => e.LanguagesInfo
                .Where(l => l.IncludeInAnonymousProfile == true)))
            .ForMember(dto => dto.CoursesInfo, o => o.MapFrom(e => e.CourseCertificates
                .Where(cs => cs.IncludeInAnonymousProfile == true)))
            .ForMember(dto => dto.SkillsInfo, o => o.MapFrom(e => e.Skills));

        configuration.CreateMap<CurriculumVitaeEntity, MyAnonymousProfileDataDTO>()
            .IncludeBase<CurriculumVitaeEntity, AnonymousProfileDataDTO>()
            .ForMember(dto => dto.ProfileAppearanceCriterias, o => o.MapFrom(e => e.AnonymousProfile.Appearance));

        configuration.CreateMap<CurriculumVitaeEntity, CVListingDTO>()
            .ForMember(dto => dto.AnonymousProfileActivated, o => o.MapFrom(e => e.AnonymousProfile != null));

        configuration.CreateMap<CurriculumVitaeEntity, MyCvDataDTO>()
            .ForMember(dto => dto.AnonymousProfileId, o => o.MapFrom(e => e.AnonymousProfile.Id));
    }
}
