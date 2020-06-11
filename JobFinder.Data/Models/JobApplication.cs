using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required]
        public string CandidateId { get; set; }

        public User Candidate { get; set; }

        public int JobAdId { get; set; }

        public JobAd JobAd { get; set; }

        public int CurriculumVitaeId { get; set; }

        public CurriculumVitae CurriculumVitae { get; set; }

        public DateTime SendedOn { get; set; }

    }
}
