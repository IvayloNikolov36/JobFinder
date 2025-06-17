namespace JobFinder.Transfer.DTOs.Cv;

public class LanguageInfoDTO
{
    public int Id { get; set; }

    public bool? IncludeInAnonymousProfile { get; set; }

    public BasicDTO LanguageType { get; set; }

    public BasicDTO ComprehensionLevel { get; set; }

    public BasicDTO SpeakingLevel { get; set; }

    public BasicDTO WritingLevel { get; set; }
}
