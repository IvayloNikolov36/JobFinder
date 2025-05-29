using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.CVModels
{
    public class CourseSertificateEditModel : IMapTo<CourseCertificateSimpleDTO>
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
