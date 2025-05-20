namespace JobFinder.DataAccess.Contracts;

public interface ICoursesCertificateInfoRepository
{
    Task SetIncludeInAnonymousProfile(string cvId, int courseCertificateInfoId);
}
