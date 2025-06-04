using JobFinder.Data.Models.Nomenclature;

namespace JobFinder.Data.Models;

public class AnonymousProfileAppearanceJobEngagementEntity
{
    public int AnonymousProfileAppearanceId { get; set; }
    public AnonymousProfileAppearanceEntity AnonymousProfileAppearance { get; set; }

    public int JobEngagementId { get; set; }
    public JobEngagementEntity JobEngagement { get; set; }
}
