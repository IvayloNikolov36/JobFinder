namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using System.ComponentModel.DataAnnotations;

    public class CourseSertificateInputModel : IMapTo<CourseCertificateEntity>
    {
        [StringLength(100, MinimumLength = 5)]
        public string CourseName { get; set; }

        [Url]
        public string CertificateUrl { get; set; }
    }
}
