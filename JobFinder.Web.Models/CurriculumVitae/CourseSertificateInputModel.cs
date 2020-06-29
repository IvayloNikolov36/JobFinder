namespace JobFinder.Web.Models.CurriculumVitae
{
    using System.ComponentModel.DataAnnotations;

    public class CourseSertificateInputModel
    {
        [Required]
        public string CvId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string CourseName { get; set; }

        [Required]
        [Url]
        public string CertificateUrl { get; set; }
    }
}
