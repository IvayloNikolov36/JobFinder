using JobFinder.Data.Models.AnonymousProfile;
using JobFinder.Data.Models.Common;
using System;

namespace JobFinder.Data.Models.Cv;

public partial class CvPreviewRequestEntity : BaseEntity<int>
{
    public string AnonymousProfileId { get; set; }
    public AnonymousProfileEntity AnonymousProfile { get; set; }

    public int JobAdId { get; set; }
    public JobAdEntity JobAd { get; set; }

    public DateTime RequestDate { get; set; }

    public DateTime? AcceptedDate { get; set; }
}
