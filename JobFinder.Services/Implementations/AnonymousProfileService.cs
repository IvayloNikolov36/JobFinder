using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.AnonymousProfile;

namespace JobFinder.Services.Implementations;

public class AnonymousProfileService : IAnonymousProfileService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public AnonymousProfileService(IEntityFrameworkUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task Activate(string cvId, AnonymousProfileCreateViewModel profile)
    {
        await this.unitOfWork.CurriculumVitaeRepository.SetAnonymousProfileCreated(cvId);

        await this.unitOfWork.WorkExperienceRepository
            .SetIncludeInAnonymousProfile(cvId, profile.WorkExpiriencesInfo);

        await this.unitOfWork.EducationInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.EducationsInfo);

        await this.unitOfWork.LanguageInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.LanguagesInfo);

        await this.unitOfWork.CoursesCertificateInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.CoursesInfo);

        await this.unitOfWork.CurriculumVitaeRepository
            .SetAnonymousProfileCreated(cvId);

        await this.unitOfWork.SaveChanges();
    }

    public async Task<AnonymousProfileCvDataViewModel> GetAnonymousProfileData(string userId)
    {
        AnonymousProfileCvDataDTO cvData = await this.unitOfWork.CurriculumVitaeRepository
            .GetAnonymousProfileCvData(userId);

        AnonymousProfileCvDataViewModel profile = this.mapper.Map<AnonymousProfileCvDataViewModel>(cvData);

        return profile;
    }
}
