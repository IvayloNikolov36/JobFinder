using JobFinder.Transfer.Common;

namespace JobFinder.Transfer.DTOs.AnonymousProfile;

public class AnonymousProfileCreateDTO : IUniquelyIdentified<string>
{
    public string Id { get; set; }

    public string CurriculumVitaeId { get; set; }

    public string UserId { get; set; }

    public AnonymousProfileAppearanceCreateDTO AppearanceDto { get; set; }

    public Guid UniqueIdentificator { get; set; }
}
