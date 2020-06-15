namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using System.ComponentModel.DataAnnotations;

    public class CourseCertificate : BaseModel<int>
    {
        public int CurriculumVitaeId { get; set; }

        public CurriculumVitae CurriculumVitae { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string CourseName { get; set; }

        [Required]
        [Url]
        public string CertificateUrl { get; set; }

    }
}
