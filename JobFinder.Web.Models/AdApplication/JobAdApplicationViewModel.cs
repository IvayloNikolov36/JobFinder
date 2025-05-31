using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;
using System;

namespace JobFinder.Web.Models.AdApplication
{
    public class JobAdApplicationViewModel : IMapFrom<JobAdApplicationDTO>
    {
        public int Id { get; set; }

        public int JobAdId { get; set; }

        public string CurriculumVitaeId { get; set; }

        public string CurriculumVitaeName { get; set; }

        public string JobTitle { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogo { get; set; }

        public DateTime AppliedOn { get; set; }

        public DateTime? PreviewDate { get; set; }
    }
}
