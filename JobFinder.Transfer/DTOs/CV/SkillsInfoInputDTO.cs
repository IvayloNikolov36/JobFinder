namespace JobFinder.Transfer.DTOs.Cv;

public class SkillsInfoInputDTO
{
    public string ComputerSkills { get; set; }

    public string Skills { get; set; }

    public bool HasManagedPeople { get; set; }

    public bool HasDrivingLicense { get; set; }

    public IEnumerable<int> DrivingLicenseCategoryIds { get; set; }
}
