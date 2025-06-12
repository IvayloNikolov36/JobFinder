using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Nomenclature;
using System.Collections.Generic;

namespace JobFinder.Data.Models.AnonymousProfile;

public partial class AnonymousProfileAppearanceEntity : BaseEntity<int>
{
    public AnonymousProfileAppearanceEntity()
    {
        this.JobEngagements = [];
        this.SoftSkills = [];
        this.ITAreas = [];
        this.TechStacks = [];
        this.WorkplaceTypes = [];
        this.Cities = [];
    }

    public string AnonymousProfileId { get; set; }
    public AnonymousProfileEntity AnonymousProfile { get; set; }

    public int JobCategoryId { get; set; }
    public JobCategoryEntity JobCategory { get; set; }

    public string PreferredPositions { get; set; }

    public List<AnonymousProfileAppearanceJobEngagementEntity> JobEngagements { get; set; }

    public List<AnonymousProfileAppearanceSoftSkillEntity> SoftSkills { get; set; }

    public List<AnonymousProfileAppearanceITAreaEntity> ITAreas { get; set; }

    public List<AnonymousProfileAppearanceTechStackEntity> TechStacks { get; set; }

    public List<AnonymousProfileAppearanceWorkplaceTypeEntity> WorkplaceTypes { get; set; }

    public List<AnonymousProfileAppearanceCityEntity> Cities { get; set; }
}
