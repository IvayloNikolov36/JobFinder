using JobFinder.Transfer.Common;

namespace JobFinder.Transfer.DTOs.CV;

public class LanguageInfoEditDTO : IIdentity
{
    public int Id { get; set; }

    public int LanguageTypeId { get; set; }

    public int ComprehensionLevelId { get; set; }

    public int SpeakingLevelId { get; set; }

    public int WritingLevelId { get; set; }
}
