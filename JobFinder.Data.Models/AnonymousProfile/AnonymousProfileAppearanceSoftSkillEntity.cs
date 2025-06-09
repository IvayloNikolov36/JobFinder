using JobFinder.Data.Models.Nomenclature;
using JobFinder.Transfer.Common;
using System;

namespace JobFinder.Data.Models.AnonymousProfile;

public class AnonymousProfileAppearanceSoftSkillEntity : IAudit
{
    public int AnonymousProfileAppearanceId { get; set; }
    public AnonymousProfileAppearanceEntity AnonymousProfileAppearance { get; set; }

    public int SoftSkillId { get; set; }
    public SoftSKillEntity SoftSkill { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
