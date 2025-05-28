namespace JobFinder.Transfer.DTOs.CV;

public class EducationInfoDTO
{
    public int Id { get; set; }

    public bool? IncludeInAnonymousProfile { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string Organization { get; set; }

    public string Location { get; set; }

    public BasicDTO EducationLevel { get; set; }

    public string Major { get; set; }

    public string MainSubjects { get; set; }
}
