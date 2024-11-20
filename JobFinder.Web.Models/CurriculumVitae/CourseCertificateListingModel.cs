namespace JobFinder.Web.Models.CurriculumVitae 
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;

    public class CourseCertificateListingModel : IMapFrom<CourseCertificate>
    {
        public string CurriculumVitaeId { get; set; }

        public string CourseName { get; set; }

        public string CertificateUrl { get; set; }
    }
}
