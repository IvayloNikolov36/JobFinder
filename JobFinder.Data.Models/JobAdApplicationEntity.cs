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

        public string CurriculumViateId { get; set; }
        public CurriculumVitaeEntity CurriculumViate { get; set; }

        public DateTime AppliedOn { get; set; }
    }
}
