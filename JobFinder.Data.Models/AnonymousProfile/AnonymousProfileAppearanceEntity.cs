using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using JobFinder.Data.Models.Nomenclature;
using System.Collections.Generic;

namespace JobFinder.Data.Models.AnonymousProfile;

public class AnonymousProfileAppearanceEntity : BaseEntity<int>
{
    public AnonymousProfileAppearanceEntity()
    {
        AnonymousProfileAppearanceJobEngagements = [];
        AnonymousProfileAppearanceSoftSkills = [];
        AnonymousProfileAppearanceITAreas = [];
        AnonymousProfileAppearanceTechStacks = [];
    }

    public string AnonymousProfileId { get; set; }
    public AnonymousProfileEntity AnonymousProfile { get; set; }

    public int RemoteJobPreferenceId { get; set; }
    public RemoteJobPreferenceEntity RemoteJobPreference { get; set; }

    public int JobCategoryId { get; set; }
    public JobCategoryEntity JobCategory { get; set; }

    public string PreferredPositions { get; set; }

    public List<AnonymousProfileAppearanceJobEngagementEntity> AnonymousProfileAppearanceJobEngagements { get; set; }

    public List<AnonymousProfileAppearanceSoftSkillEntity> AnonymousProfileAppearanceSoftSkills { get; set; }

    public List<AnonymousProfileAppearanceITAreaEntity> AnonymousProfileAppearanceITAreas { get; set; }

    public List<AnonymousProfileAppearanceTechStackEntity> AnonymousProfileAppearanceTechStacks { get; set; }
}
