using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.CV;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class CurriculumVitaeRepository : EfCoreRepository<CurriculumVitaeEntity>, ICurriculumVitaeRepository
{
    private readonly IMapper mapper;

    public CurriculumVitaeRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<string> GetUserId(string curriculumVitaeId)
    {
        string userId = await this.DbSet
            .Where(cv => cv.Id == curriculumVitaeId)
            .Select(cv => cv.UserId)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(userId, "CurriculumVitae");

        return userId;
    }

    public async Task<AnonymousProfileCvDataDTO> GetAnonymousProfileCvData(string userId)
    {
        return await this.DbSet
            .Where(cv => cv.UserId == userId && cv.AnonymousProfileActivated)
            .Select(cv => new AnonymousProfileCvDataDTO
            {
                // TODO: fix the mappings -> BasicDTOs are null
                Id = cv.Id,
                PersonalDetails = this.mapper.Map<PersonalInfoDTO>(cv.PersonalDetails),
                WorkExperienceInfo = this.mapper.Map<IEnumerable<WorkExperienceInfoDTO>>(
                    cv.WorkExperiences.Where(we => we.IncludeInAnonymousProfile == true)),
                EducationInfo = this.mapper.Map<IEnumerable<EducationInfoDTO>>(
                    cv.Educations.Where(e => e.IncludeInAnonymousProfile == true)),
                LanguagesInfo = this.mapper.Map<IEnumerable<LanguageInfoDTO>>(
                    cv.LanguagesInfo.Where(li => li.IncludeInAnonymousProfile == true)),
                CourseCertificates = this.mapper.Map<IEnumerable<CourseCertificateDTO>>(
                    cv.CourseCertificates.Where(cs => cs.IncludeInAnonymousProfile == true)),
                SkillsInfo = this.mapper.Map<SkillsInfoDTO>(cv.Skills)
            })
            .SingleOrDefaultAsync();
    }
}
