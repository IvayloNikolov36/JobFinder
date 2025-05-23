using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.CV;
using System.Collections.Generic;

namespace JobFinder.Web.Models.AnonymousProfile;

public class AnonymousProfileCvDataViewModel : IMapFrom<AnonymousProfileCvDataDTO>
{
    public string Id { get; set; }

    public PersonalInfoDTO PersonalDetails { get; set; }

    public IEnumerable<EducationInfoDTO> EducationInfo { get; set; }

    public IEnumerable<WorkExperienceInfoDTO> WorkExperienceInfo { get; set; }

    public IEnumerable<LanguageInfoDTO> LanguagesInfo { get; set; }

    public SkillsInfoDTO SkillsInfo { get; set; }

    public IEnumerable<CourseCertificateDTO> CourseCertificates { get; set; }
}
