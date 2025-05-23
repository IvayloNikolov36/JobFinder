using AutoMapper;
using JobFinder.Common.Exceptions;
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
    }

    public async Task Create(string userId, AnonymousProfileCreateViewModel profile)
    {
        string cvUserId = await this.unitOfWork
            .CurriculumVitaeRepository
            .GetUserId(profile.CurriculumVitaeId);

        if (userId != cvUserId)
        {
            throw new ActionableException("You can activate anonymous profile for foreign curriculum vitae!");
        }

        // TODO: set in cv entity that has created anonymous profile (set bit to 1)
        
        foreach (int workExperienceInfoId in profile.WorkExpiriencesInfo)
        {
            await this.unitOfWork.WorkExperienceRepository
                .SetIncludeInAnonymousProfile(profile.CurriculumVitaeId, workExperienceInfoId);
        }

        foreach (int educationInfoId in profile.EducationsInfo)
        {
            await this.unitOfWork.EducationInfoRepository
                .SetIncludeInAnonymousProfile(profile.CurriculumVitaeId, educationInfoId);
        }

        foreach (int languageInfoId in profile.LanguagesInfo)
        {
            await this.unitOfWork.LanguageInfoRepository
                .SetIncludeInAnonymousProfile(profile.CurriculumVitaeId, languageInfoId);
        }

        foreach (int courseInfoId in profile.CoursesInfo)
        {
            await this.unitOfWork.CoursesCertificateInfoRepository
                .SetIncludeInAnonymousProfile(profile.CurriculumVitaeId, courseInfoId);
        }

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
