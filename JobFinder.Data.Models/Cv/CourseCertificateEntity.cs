using JobFinder.Data.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models.CV
{
    public partial class CourseCertificateEntity : BaseEntity<int>
    {
        [Required]
        public string CurriculumVitaeId { get; set; }
        public CurriculumVitaeEntity CurriculumVitae { get; set; }

        public bool? IncludeInAnonymousProfile { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string CourseName { get; set; }

        [Required]
        [Url]
        public string CertificateUrl { get; set; }
    }
}
