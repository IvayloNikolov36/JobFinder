namespace JobFinder.DataAccess.Contracts;

public interface ICoursesCertificateInfoRepository
{
    Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> courseCertificateInfoIds);

    Task DisassociateFromAnonymousProfile(string cvId);
}
