using AutoMapper;
using JobFinder.Business.CurriculumVitaes;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using JobFinder.Web.Models.AnonymousProfile;

namespace JobFinder.Services.Implementations;

public class AnonymousProfileService : IAnonymousProfileService
{
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ICurriculumVitaesRules cvRules;

    public AnonymousProfileService(
        IEntityFrameworkUnitOfWork unitOfWork,
        IMapper mapper,
        ICurriculumVitaesRules cvRules)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.cvRules = cvRules;
    }

    public async Task<string> Create(string cvId, string userId, AnonymousProfileCreateViewModel profile)
    {
        bool hasAlreadyAnActivated = await this.unitOfWork.AnonymousProfileRepository
            .HasAnonymousProfile(userId);

        this.cvRules.ValidateAnonymousProfileCreation(hasAlreadyAnActivated);

        await this.unitOfWork.WorkExperienceRepository
            .SetIncludeInAnonymousProfile(cvId, profile.WorkExpiriencesInfo);

        await this.unitOfWork.EducationInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.EducationsInfo);

        await this.unitOfWork.LanguageInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.LanguagesInfo);

        await this.unitOfWork.CoursesCertificateInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.CoursesInfo);

        AnonymousProfileCreateDTO anonymousProfileDto = new AnonymousProfileCreateDTO
        {
            UserId = userId,
            CurriculumVitaeId = cvId,
            AppearanceDto = this.mapper
                .Map<AnonymousProfileAppearanceDTO>(profile.ProfileAppearanceCriterias)
        };

        await this.unitOfWork.AnonymousProfileRepository.Create(cvId, userId, anonymousProfileDto);

        await this.unitOfWork.SaveChanges<AnonymousProfileCreateDTO, string>(anonymousProfileDto);

        return anonymousProfileDto.Id;
    }

    public async Task Delete(string id)
    {
        string cvId = await this.unitOfWork.AnonymousProfileRepository.GetCvId(id);

        await this.unitOfWork.WorkExperienceRepository.DisassociateFromAnonymousProfile(cvId);
        await this.unitOfWork.EducationInfoRepository.DisassociateFromAnonymousProfile(cvId);
        await this.unitOfWork.LanguageInfoRepository.DisassociateFromAnonymousProfile(cvId);
        await this.unitOfWork.CoursesCertificateInfoRepository.DisassociateFromAnonymousProfile(cvId);

        await this.unitOfWork.AnonymousProfileRepository.Delete(id);

        await this.unitOfWork.SaveChanges();
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
}
