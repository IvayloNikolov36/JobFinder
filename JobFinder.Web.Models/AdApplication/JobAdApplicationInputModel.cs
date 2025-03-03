using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.AdApplication
{
    public class JobAdApplicationInputModel : IMapTo<JobAdApplicationEntity>
    {
        [Required]
        public int JobAdId { get; set; }

        [Required]
        public string CurriculumVitaeId { get; set; }

        public string ApplicantId { get; set; }
    }
}
