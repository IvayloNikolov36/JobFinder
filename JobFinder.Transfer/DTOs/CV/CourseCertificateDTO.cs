namespace JobFinder.Transfer.DTOs.CV;

public class CourseCertificateDTO
{
    public int Id { get; set; }

    public bool? IncludeInAnonymousProfile { get; set; }

    public string CourseName { get; set; }

    public string CertificateUrl { get; set; }
}
