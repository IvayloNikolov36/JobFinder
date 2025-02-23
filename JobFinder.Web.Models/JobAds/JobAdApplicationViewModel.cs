using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using System;

namespace JobFinder.Web.Models.JobAds
{
    public class JobAdApplicationViewModel : IMapFrom<JobAdApplicationEntity>
    {
        public int Id { get; set; }

        public int JobAdId { get; set; }

        public string CurriculumVitaeId { get; set; }

        public string CurriculumVitaeName { get; set; }

        public DateTime AppliedOn { get; set; }
    }
}
