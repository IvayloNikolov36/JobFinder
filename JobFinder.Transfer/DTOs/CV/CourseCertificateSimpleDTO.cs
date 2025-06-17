using JobFinder.Transfer.Common;

namespace JobFinder.Transfer.DTOs.Cv;

public class CourseCertificateSimpleDTO : IIdentity
{
    public int Id { get; set; }

    public string CourseName { get; set; }

    public string CertificateUrl { get; set; }
}
