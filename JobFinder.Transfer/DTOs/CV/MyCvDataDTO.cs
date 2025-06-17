namespace JobFinder.Transfer.DTOs.Cv;

public class MyCvDataDTO
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string PictureUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public string AnonymousProfileId { get; set; }

    public PersonalInfoDTO PersonalInfo { get; set; }

    public IEnumerable<EducationInfoDTO> Educations { get; set; }

    public IEnumerable<WorkExperienceInfoDTO> WorkExperiences { get; set; }

    public IEnumerable<LanguageInfoDTO> LanguagesInfo { get; set; }

    public SkillsInfoDTO Skills { get; set; }

    public IEnumerable<CourseCertificateDTO> CourseCertificates { get; set; }
}
