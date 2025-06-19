using JobFinder.Data.Models.Nomenclature;
using JobFinder.Transfer.Common;
using System;

namespace JobFinder.Data.Models;

public class JobAdItAreaEntity : IAudit
{
    public int JobAdId { get; set; }
    public JobAdEntity JobAd { get; set; }

    public int ItAreaId { get; set; }
    public ItAreaEntity ItArea { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
