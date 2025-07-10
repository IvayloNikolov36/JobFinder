using AutoMapper;
using JobFinder.Business.AnonymousProfile;
using JobFinder.Business.Cv;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;
using JobFinder.Transfer.DTOs.JobAd;
using JobFinder.Web.Models.AnonymousProfile;
using JobFinder.Web.Models.CvModels;
using JobFinder.Web.Models.JobAds;
using Microsoft.Extensions.Caching.Distributed;
using static JobFinder.Services.Constants.CacheConstants;


namespace JobFinder.Services.Implementations;

public class AnonymousProfileService : IAnonymousProfileService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ICvRules cvRules;
    private readonly IAnonymousProfileRules anonymousProfileRules;
    private readonly IDistributedCache distributedCache;

    public AnonymousProfileService(
        IEntityFrameworkUnitOfWork unitOfWork,
        IMapper mapper,
        ICvRules cvRules,
        IAnonymousProfileRules anonymousProfileRules,
        IDistributedCache distributedCache)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.cvRules = cvRules;
        this.anonymousProfileRules = anonymousProfileRules;
        this.distributedCache = distributedCache;
    }

    public async Task<string> Create(
        string cvId,
        string userId,
        AnonymousProfileCreateViewModel profile)
    {
        bool hasAlreadyAnActivated = await this.unitOfWork.AnonymousProfileRepository
            .HasAnonymousProfile(userId);

        this.cvRules.ValidateAnonymousProfileCanBeCreated(hasAlreadyAnActivated);

        AnonymousProfileAppearanceDTO appearanceDto = this.mapper
            .Map<AnonymousProfileAppearanceDTO>(profile.ProfileAppearanceCriterias);

        this.anonymousProfileRules.ValidateAnonymousProfileAppearanceData(appearanceDto);

        await this.unitOfWork.WorkExperienceRepository
            .SetIncludeInAnonymousProfile(cvId, profile.WorkExpiriencesInfo);

        await this.unitOfWork.EducationInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.EducationsInfo);

        await this.unitOfWork.LanguageInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.LanguagesInfo);

        await this.unitOfWork.CoursesCertificateInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.CoursesInfo);

        AnonymousProfileCreateDTO anonymousProfileDto = new()
        {
            UserId = userId,
            CvId = cvId,
            AppearanceDto = appearanceDto
        };

        await this.unitOfWork.AnonymousProfileRepository.Create(cvId, userId, anonymousProfileDto);

        await this.unitOfWork.SaveChanges<AnonymousProfileCreateDTO, string>(anonymousProfileDto);

        await this.RemoveCvFromCache(cvId);
        await this.RemoveCVsFromCache(userId);

        return anonymousProfileDto.Id;
    }

    public async Task Delete(string anonymousProfileId, string userId)
    {
        string cvId = await this.unitOfWork
            .AnonymousProfileRepository
            .GetCvId(anonymousProfileId);

        await this.unitOfWork.WorkExperienceRepository.DisassociateFromAnonymousProfile(cvId);
        await this.unitOfWork.EducationInfoRepository.DisassociateFromAnonymousProfile(cvId);
        await this.unitOfWork.LanguageInfoRepository.DisassociateFromAnonymousProfile(cvId);
        await this.unitOfWork.CoursesCertificateInfoRepository.DisassociateFromAnonymousProfile(cvId);

        await this.unitOfWork.AnonymousProfileRepository.Delete(anonymousProfileId);

        await this.unitOfWork.SaveChanges();

        await this.RemoveCvFromCache(cvId);
        await this.RemoveCVsFromCache(userId);
    }

    public async Task<AnonymousProfileDataViewModel> GetAnonymousProfile(string anonymousProfileId)
    {
        AnonymousProfileDataDTO cvData = await this.unitOfWork.AnonymousProfileRepository
            .GetAnonymousProfile(anonymousProfileId);

        AnonymousProfileDataViewModel profile = this.mapper.Map<AnonymousProfileDataViewModel>(cvData);

        return profile;
    }

    public async Task<MyAnonymousProfileDataViewModel> GetMyAnonymousProfileData(string userId)
    {
        MyAnonymousProfileDataDTO cvData = await this.unitOfWork.AnonymousProfileRepository
            .GetMyAnonymousProfileData(userId);

        MyAnonymousProfileDataViewModel profile = this.mapper.Map<MyAnonymousProfileDataViewModel>(cvData);

        return profile;
    }

    public async Task<string> GetOwnerId(string id)
    {
        return await this.unitOfWork.AnonymousProfileRepository.GetOwnerId(id);
    }

    public async Task<bool> IsRelevant(string id, JobAdCriteriasViewModel jobAdCriterias)
    {
        return await this.unitOfWork
            .AnonymousProfileRepository
            .IsAnonymousProfileRelevantForJobAd(
                id,
                this.mapper.Map<JobAdCriteriasDTO>(jobAdCriterias));
    }

    private async Task RemoveCvFromCache(string cvId)
    {
        string cvCacheKey = string.Format(CvCacheKey, cvId);
        await this.distributedCache.RemoveAsync(cvCacheKey);
    }

    private async Task RemoveCVsFromCache(string userId)
    {
        string cvsCacheKey = string.Format(CVsCacheKey, userId);
        await this.distributedCache.RemoveAsync(cvsCacheKey);
    }
}
