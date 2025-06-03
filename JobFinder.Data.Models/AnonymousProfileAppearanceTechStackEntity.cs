using JobFinder.Data.Models.Cv;
using JobFinder.Data.Models.Nomenclature;

namespace JobFinder.Data.Models;

public class AnonymousProfileAppearanceTechStackEntity
{
    public int AnonymousProfileAppearanceId { get; set; }
    public AnonymousProfileAppearanceEntity AnonymousProfileAppearance { get; set; }

    public int TechStackId { get; set; }
    public TechStackEntity TechStack { get; set; }
}
