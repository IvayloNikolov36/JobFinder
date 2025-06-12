using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Nomenclature;

namespace JobFinder.Data.Models.AnonymousProfile;

public class AnonymousProfileAppearanceWorkplaceTypeEntity : BaseEntity<int>
{
    public int AnonymousProfileAppearanceId { get; set; }
    public AnonymousProfileAppearanceEntity AnonymousProfileAppearance { get; set; }

    public int WorkplaceTypeId { get; set; }
    public WorkplaceTypeEntity WorkplaceType { get; set; }
}
