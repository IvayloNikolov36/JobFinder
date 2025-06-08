using JobFinder.Data.Models.Nomenclature;
using JobFinder.Transfer.Common;
using System;

namespace JobFinder.Data.Models;

public class JobAdvertisementITAreaEntity : IAudit
{
    public int JobAdvertisementId { get; set; }
    public JobAdvertisementEntity JobAdvertisement { get; set; }

    public int ITAreaId { get; set; }
    public ITAreaEntity ITArea { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
