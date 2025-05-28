using JobFinder.Data.Models.CV;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.Web.Models.CVModels;

public class CourseInfoViewModel : IMapFrom<CourseCertificateEntity>, IMapFrom<CourseCertificateDTO>
{
    public int Id { get; set; }

    public bool? IncludeInAnonymousProfile { get; set; }

    public string CourseName { get; set; }

    public string CertificateUrl { get; set; }
}
