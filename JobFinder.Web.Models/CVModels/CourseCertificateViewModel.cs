namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;

    public class CourseCertificateViewModel : IMapFrom<CourseCertificate>
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string CertificateUrl { get; set; }
    }
}
