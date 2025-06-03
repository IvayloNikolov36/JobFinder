using JobFinder.Data.Models.Cv;
using JobFinder.Data.Models.Nomenclature;

namespace JobFinder.Data.Models;

public class AnonymousProfileAppearanceITAreaEntity
{
    public int AnonymousProfileAppearanceId { get; set; }
    public AnonymousProfileAppearanceEntity AnonymousProfileAppearance { get; set; }

    public int ITAreaId { get; set; }
    public ITAreaEntity ITArea { get; set; }
}
