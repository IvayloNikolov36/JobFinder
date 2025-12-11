using JobFinder.Transfer.Common;

namespace JobFinder.Transfer.DTOs.Cv;

public class CourseCertificateSimpleDTO : IUniquelyIdentified<int>
{
    public int Id { get; set; }

    public string CourseName { get; set; }

    public string CertificateUrl { get; set; }

    public Guid UniqueIdentificator { get; set; }
}
