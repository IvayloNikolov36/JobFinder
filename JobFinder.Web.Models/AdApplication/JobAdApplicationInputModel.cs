using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.AdApplication
{
    public class JobAdApplicationInputModel : IMapTo<JobAddApplicationInputDTO>
    {
        [Required]
        public int JobAdId { get; set; }

        [Required]
        public string CvId { get; set; }

        public string ApplicantId { get; set; }
    }
}
