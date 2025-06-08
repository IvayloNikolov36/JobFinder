using JobFinder.Data.Models.Nomenclature;
using JobFinder.Transfer.Common;
using System;

namespace JobFinder.Data.Models;

public class AnonymousProfileAppearanceJobEngagementEntity : IAudit
{
    public int AnonymousProfileAppearanceId { get; set; }
    public AnonymousProfileAppearanceEntity AnonymousProfileAppearance { get; set; }

    public int JobEngagementId { get; set; }
    public JobEngagementEntity JobEngagement { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
