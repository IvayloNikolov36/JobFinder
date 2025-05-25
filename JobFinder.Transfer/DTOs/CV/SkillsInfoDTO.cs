namespace JobFinder.Transfer.DTOs.CV;

public class SkillsInfoDTO
{
    public int Id { get; set; }

    public string ComputerSkills { get; set; }

    public string OtherSkills { get; set; }

    public bool HasManagedPeople { get; set; }

    public bool HasDrivingLicense { get; set; }

    public IEnumerable<BasicDTO> DrivingLicenseCategories { get; set; }
}
