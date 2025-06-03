using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using JobFinder.Data.Models.Nomenclature;
using System.Collections.Generic;

namespace JobFinder.Data.Models.Cv;

public class AnonymousProfileAppearanceEntity : BaseEntity<int>
{
    public AnonymousProfileAppearanceEntity()
    {
        this.AnonymousProfileAppearanceSoftSkills = new HashSet<AnonymousProfileAppearanceSoftSkillEntity>();
        this.AnonymousProfileAppearanceITAreas = new HashSet<AnonymousProfileAppearanceITAreaEntity>();
        this.AnonymousProfileAppearanceTechStacks = new HashSet<AnonymousProfileAppearanceTechStackEntity>();
    }

    public string CurriculumVitaeId { get; set; }
    public CurriculumVitaeEntity CurriculumVitae { get; set; }

    public int RemoteJobPreferenceId { get; set; }
    public RemoteJobPreferenceEntity RemoteJobPreference { get; set; }

    public int JobCategoryId { get; set; }
    public JobCategoryEntity JobCategory { get; set; }

    public int JobEngagementId { get; set; }
    public JobEngagementEntity JobEngagements { get; set; }

    public string PreferredPositions { get; set; }

    public ICollection<AnonymousProfileAppearanceSoftSkillEntity> AnonymousProfileAppearanceSoftSkills { get; set; }

    public ICollection<AnonymousProfileAppearanceITAreaEntity> AnonymousProfileAppearanceITAreas { get; set; }

    public ICollection<AnonymousProfileAppearanceTechStackEntity> AnonymousProfileAppearanceTechStacks { get; set; }
}
