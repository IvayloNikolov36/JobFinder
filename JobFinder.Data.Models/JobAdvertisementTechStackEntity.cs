using JobFinder.Data.Models.Nomenclature;
using JobFinder.Transfer.Common;
using System;

namespace JobFinder.Data.Models;

public class JobAdvertisementTechStackEntity : IAudit
{
    public int JobAdvertisementId { get; set; }
    public JobAdvertisementEntity JobAdvertisement { get; set; }

    public int TechStackId { get; set; }
    public TechStackEntity TechStack { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
