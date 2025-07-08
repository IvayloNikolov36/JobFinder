using JobFinder.Data.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.Cv;

public partial class CvPreviewRequestEntity : BaseEntity<int>
{
    public string RequesterId { get; set; }
    public UserEntity Requester { get; set; }

    [Required]
    public string CvId { get; set; }
    public CurriculumVitaeEntity Cv { get; set; }

    public int JobAdId { get; set; }
    public JobAdEntity JobAd { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
