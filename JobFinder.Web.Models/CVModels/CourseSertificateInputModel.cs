using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.CvModels
{
    public class CourseSertificateInputModel : IMapTo<CourseCertificateInputDTO>
    {
        [StringLength(100, MinimumLength = 5)]
        public string CourseName { get; set; }

        [Url]
        public string CertificateUrl { get; set; }
    }
}
