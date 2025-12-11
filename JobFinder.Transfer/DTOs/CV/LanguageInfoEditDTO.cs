using JobFinder.Transfer.Common;

namespace JobFinder.Transfer.DTOs.Cv;

public class LanguageInfoEditDTO : IUniquelyIdentified<int>
{
    public int Id { get; set; }

    public int LanguageTypeId { get; set; }

    public int ComprehensionLevelId { get; set; }

    public int SpeakingLevelId { get; set; }

    public int WritingLevelId { get; set; }

    public Guid UniqueIdentificator { get; set; }
}
