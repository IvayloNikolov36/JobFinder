namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using System.ComponentModel.DataAnnotations;

    public class CourseSertificateEditModel : IMapTo<CourseCertificate>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string CourseName { get; set; }

        [Required]
        [Url]
        public string CertificateUrl { get; set; }
    }
}
