using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Candidates
{
    public class CurriculumVitaeUploadModel
    {
        public string CandidateId { get; set; }

        [Required]
        public IFormFile CurriculumVitae { get; set; }
    }
}
