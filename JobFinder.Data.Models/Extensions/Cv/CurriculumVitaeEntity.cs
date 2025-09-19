using AutoMapper;
using JobFinder.Data.Models.Enums;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using JobFinder.Transfer.DTOs.Cv;
using System.Linq;

namespace JobFinder.Data.Models.Cv;

public partial class CurriculumVitaeEntity :
    IMapTo<CvPreviewDataDTO>,
    IMapTo<CvBasicDetailsDTO>,
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

        int jobAdId = 0;
        string requesterId = string.Empty;

        configuration.CreateMap<CurriculumVitaeEntity, CompanyAnonymousProfileDataDTO>()
            .IncludeBase<CurriculumVitaeEntity, AnonymousProfileDataDTO>()
            .ForMember(dto => dto.IsCvRequested, o => o.MapFrom(e => e.CvPreviewRequests
                .Any(cr => cr.JobAdId == jobAdId && cr.RequesterId == requesterId)));

        configuration.CreateMap<CurriculumVitaeEntity, MyAnonymousProfileDataDTO>()
            .IncludeBase<CurriculumVitaeEntity, AnonymousProfileDataDTO>()
            .ForMember(dto => dto.ProfileAppearanceCriterias, o => o.MapFrom(e => e.AnonymousProfile.Appearance));

        configuration.CreateMap<CurriculumVitaeEntity, CVListingDTO>()
            .ForMember(dto => dto.AnonymousProfileActivated, o => o.MapFrom(e => e.AnonymousProfile != null));

        configuration.CreateMap<CurriculumVitaeEntity, MyCvDataDTO>()
            .ForMember(dto => dto.AnonymousProfileId, o => o.MapFrom(e => e.AnonymousProfile.Id))
            .ForMember(dto => dto.ApplicationForActiveAd, o => o.MapFrom(e => e.JobAdApplications
                .Any(a => a.JobAd.LifecycleStatusId == (int)LifecycleStatusEnum.Active)))
            .ForMember(dto => dto.ApprovedCvPreviewForActiveAd, o => o.MapFrom(e => e.CvPreviewRequests
                .Any(r => r.AcceptedDate.HasValue
                    && r.JobAd.LifecycleStatusId == (int)LifecycleStatusEnum.Active))
            );
    }
}
