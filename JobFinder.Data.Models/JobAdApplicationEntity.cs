using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.CV;
using System;

namespace JobFinder.Data.Models
{
    public class JobAdApplicationEntity : BaseEntity<int>
    {
        public int JobAdId { get; set; }
        public JobAdvertisementEntity JobAd { get; set; }

        public string ApplicantId { get; set; }
        public UserEntity Applicant { get; set; }

        public string CurriculumVitaeId { get; set; }
        public CurriculumVitaeEntity CurriculumVitae { get; set; }

        public DateTime AppliedOn { get; set; }

        public DateTime? FirstPreviewDate { get; set; }

        public DateTime? LatestPreviewDate { get; set; }
    }
}
