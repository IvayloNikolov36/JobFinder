using JobFinder.Data.Models.Nomenclature;
using JobFinder.Transfer.Common;
using System;

namespace JobFinder.Data.Models;

public class JobAdTechStackEntity : IAudit
{
    public int JobAdId { get; set; }
    public JobAdEntity JobAd { get; set; }

    public int TechStackId { get; set; }
    public TechStackEntity TechStack { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
