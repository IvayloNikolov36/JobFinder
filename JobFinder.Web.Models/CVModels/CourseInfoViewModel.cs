using JobFinder.Data.Models.Cv;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.Web.Models.CvModels;

public class CourseInfoViewModel : IMapFrom<CourseCertificateEntity>, IMapFrom<CourseCertificateDTO>
{
    public int Id { get; set; }

    public bool? IncludeInAnonymousProfile { get; set; }

    public string CourseName { get; set; }

    public string CertificateUrl { get; set; }
}
