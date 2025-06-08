using JobFinder.Data.Models.Nomenclature;
using JobFinder.Transfer.Common;
using System;

namespace JobFinder.Data.Models;

public class JobAdvertisementSoftSkillEntity : IAudit
{
    public int JobAdvertisementId { get; set; }
    public JobAdvertisementEntity JobAdvertisement { get; set; }

    public int SoftSkillId { get; set; }
    public SoftSKillEntity SoftSkill { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}