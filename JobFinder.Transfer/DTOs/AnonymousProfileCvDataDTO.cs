using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.Transfer.DTOs;

public class AnonymousProfileCvDataDTO
{
    public required string Id { get; set; }

    public required PersonalInfoDTO PersonalDetails { get; set; }

    public required IEnumerable<EducationInfoDTO> EducationInfo { get; set; }

    public required IEnumerable<WorkExperienceInfoDTO> WorkExperienceInfo { get; set; }

    public required IEnumerable<LanguageInfoDTO> LanguagesInfo { get; set; }

    public required SkillsInfoDTO SkillsInfo { get; set; }

    public required IEnumerable<CourseCertificateDTO> CourseCertificates { get; set; }
}
