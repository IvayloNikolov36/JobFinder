namespace JobFinder.Transfer.DTOs.Cv;

public class CvPreviewDataDTO
{
    public string PictureUrl { get; set; }

    public PersonalInfoDTO PersonalInfo { get; set; }

    public IEnumerable<EducationInfoDTO> Educations { get; set; }

    public IEnumerable<WorkExperienceInfoDTO> WorkExperiences { get; set; }

    public IEnumerable<LanguageInfoDTO> LanguagesInfo { get; set; }

    public SkillsInfoDTO Skills { get; set; }

    public IEnumerable<CourseCertificateDTO> CourseCertificates { get; set; }
}
