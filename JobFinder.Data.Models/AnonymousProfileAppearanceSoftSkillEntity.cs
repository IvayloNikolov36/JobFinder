using JobFinder.Data.Models.Nomenclature;

namespace JobFinder.Data.Models;

public class AnonymousProfileAppearanceSoftSkillEntity
{
    public int AnonymousProfileAppearanceId { get; set; }
    public AnonymousProfileAppearanceEntity AnonymousProfileAppearance { get; set; }

    public int SoftSkillId { get; set; }
    public SoftSKillEntity SoftSkill { get; set; }
}
