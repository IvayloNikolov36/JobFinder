namespace JobFinder.Transfer.DTOs.Cv;

public class WorkExperienceInfoDTO
{
    public int Id { get; set; }

    public bool? IncludeInAnonymousProfile { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string JobTitle { get; set; }

    public string Organization { get; set; }

    public BasicDTO BusinessSector { get; set; }

    public string Location { get; set; }

    public string AdditionalDetails { get; set; }
}
