using JobFinder.Data.Models.Nomenclature;
using JobFinder.Transfer.Common;
using System;

namespace JobFinder.Data.Models.AnonymousProfile;

public class AnonymousProfileAppearanceITAreaEntity : IAudit
{
    public int AnonymousProfileAppearanceId { get; set; }
    public AnonymousProfileAppearanceEntity AnonymousProfileAppearance { get; set; }

    public int ITAreaId { get; set; }
    public ITAreaEntity ITArea { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
