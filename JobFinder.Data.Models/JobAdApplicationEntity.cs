using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Cv;
using System;

namespace JobFinder.Data.Models
{
    public partial class JobAdApplicationEntity : BaseEntity<int>
    {
        public int JobAdId { get; set; }
        public JobAdvertisementEntity JobAd { get; set; }

        public string ApplicantId { get; set; }
        public UserEntity Applicant { get; set; }

        public string CvId { get; set; }
        public CurriculumVitaeEntity Cv { get; set; }

        public DateTime AppliedOn { get; set; }

        public DateTime? PreviewDate { get; set; }
    }
}
