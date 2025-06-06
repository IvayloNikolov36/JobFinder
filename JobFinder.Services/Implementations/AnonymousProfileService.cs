using AutoMapper;
using JobFinder.Business.CurriculumVitaes;
using JobFinder.Common.Exceptions;
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

    public async Task Activate(string cvId, string userId, AnonymousProfileCreateViewModel profile)
    {
        bool hasAlreadyAnActivated = await this.unitOfWork.CurriculumVitaeRepository
            .HasAnyCvWithActivatedAnonymousProfile(userId);

        this.cvRules.ValidateAnonymousProfileCreation(hasAlreadyAnActivated);

        await this.unitOfWork.WorkExperienceRepository
            .SetIncludeInAnonymousProfile(cvId, profile.WorkExpiriencesInfo);

        await this.unitOfWork.EducationInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.EducationsInfo);

        await this.unitOfWork.LanguageInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.LanguagesInfo);

        await this.unitOfWork.CoursesCertificateInfoRepository
            .SetIncludeInAnonymousProfile(cvId, profile.CoursesInfo);

        AnonymousProfileAppearanceCreateDTO appearanceDto = this.mapper
            .Map<AnonymousProfileAppearanceCreateDTO>(profile.ProfileAppearanceCriterias);

        await this.unitOfWork.AnonymousProfileAppearanceRepository.Create(cvId, appearanceDto);

        await this.unitOfWork.CurriculumVitaeRepository.SetAnonymousProfileActivated(cvId);

        await this.unitOfWork.SaveChanges();
    }

    public async Task Deactivate(string cvId)
    {
        bool hasAlreadyActivated = await this.unitOfWork.CurriculumVitaeRepository
            .HasActivatedAnonymousProfile(cvId);

        if (!hasAlreadyActivated)
        {
            throw new ActionableException("You don't have an activated anonymous profile!");
        }

        await this.unitOfWork.WorkExperienceRepository.DisassociateFromAnonymousProfile(cvId);
        await this.unitOfWork.EducationInfoRepository.DisassociateFromAnonymousProfile(cvId);
        await this.unitOfWork.LanguageInfoRepository.DisassociateFromAnonymousProfile(cvId);
        await this.unitOfWork.CoursesCertificateInfoRepository.DisassociateFromAnonymousProfile(cvId);

        await this.unitOfWork.AnonymousProfileAppearanceRepository.Delete(cvId);

        await this.unitOfWork.CurriculumVitaeRepository.DeactivateAnonymousProfile(cvId);

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
