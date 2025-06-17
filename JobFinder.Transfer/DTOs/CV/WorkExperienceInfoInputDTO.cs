namespace JobFinder.Transfer.DTOs.Cv;

public class WorkExperienceInfoInputDTO
{
    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string JobTitle { get; set; }

    public string Organization { get; set; }

    public int BusinessSectorId { get; set; }

    public string Location { get; set; }

    public string AdditionalDetails { get; set; }
}
