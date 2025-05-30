using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.DataAccess.Contracts;

public interface ICoursesCertificateInfoRepository
{
    Task<IEnumerable<CourseCertificateDTO>> All(string cvId);

    Task<IEnumerable<CourseCertificateSimpleDTO>> Update(
        string cvId,
        IEnumerable<CourseCertificateSimpleDTO> courcesInfo);

    Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> courseCertificateInfoIds);

    Task DisassociateFromAnonymousProfile(string cvId);

    Task Delete(int id);

    void Delete(string cvId);
}
