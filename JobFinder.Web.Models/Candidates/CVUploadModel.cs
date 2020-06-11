namespace JobFinder.Web.Models.Candidates
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class CVUploadModel
    {
        public string CandidateId { get; set; }

        [Required]
        public IFormFile CurriculumVitae { get; set; }
    }
}
